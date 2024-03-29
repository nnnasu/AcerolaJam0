using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Abilities.Enums;
using Core.Animation;
using PrimeTween;
using UnityEngine;

namespace Core.Abilities.Instances {
    [Serializable]
    public class AbilityInstance {

        [HideInInspector]
        public AbilityManager owner;

        public List<ActionInstance> actions = new();
        public List<ModifierInstance> modifiers = new();

        [Header("Current State")]
        public float CooldownDisplay;
        public float UsageTimeDisplay;
        public bool isOnCooldown { get; private set; } = false;
        public float cachedCooldownTime { get; private set; }
        public float cachedUsageTime { get; private set; }
        public float baseUsageTime { get; private set; } = 0;
        public float cachedMPCost { get; private set; }

        public bool useDynamicCost = false;
        public bool useDynamicCooldown = false;
        public Tween CooldownTween; // sum(action cd)
        public Tween UsageTween; // cast time = max(action cast time)

        // Animations
        public AnimationStateInfo StateToPlay { get; private set; } = null;
        public string CachedDescription = "";

        public event Action OnAbilityActivated = delegate { };
        public event Action<bool> OnFocusChanged = delegate { };
        public event Action<float> OnCooldownStarted = delegate { };
        public event Action OnCooldownEnded = delegate { };
        public event Action OnAbilityChanged = delegate { };

        public AbilityInstance(AbilityManager owner) {
            this.owner = owner;
        }

        public void OnAbilityModified() {
            CalculateCooldownTime(owner);
            CalculateUsageTime(owner);
            CalculateMPCost(owner);
            RecalculateAnimation();
            RegenerateDescription();
            OnAbilityChanged?.Invoke();
        }

        public bool SetFocus(bool focused) {
            // TODO FIX THIS // never mind I think it is fixed already idk
            if (!focused) {
                OnFocusChanged?.Invoke(false);
                return true;
            }
            if (isOnCooldown) {
                OnFocusChanged?.Invoke(false);
                return false;
            }
            OnFocusChanged?.Invoke(true);
            return true;
        }

        public bool ActivateAbility(Vector3 targetPoint) {
            if (isOnCooldown) return false;
            if (useDynamicCooldown) cachedCooldownTime = CalculateCooldownTime(owner); // otherwise, just use cached.
            if (useDynamicCost) cachedMPCost = CalculateMPCost(owner);
            if (owner.Attributes.MP - cachedMPCost < 0) return false;

            foreach (var item in actions) {
                item.ActivateAbility(owner, this, targetPoint, OnHit);
            }

            foreach (var item in modifiers) {
                item.OnActivate(owner, this, targetPoint);
            }
            // Start Cooldowns
            OnAbilityActivated?.Invoke();
            isOnCooldown = true;

            if (actions.Count > 0 && actions[0].definition.actionType == ActionType.BasicAttack) {
                CalculateUsageTime(owner);
            }
            owner.Attributes.CostMana(cachedMPCost);

            if (cachedUsageTime <= float.Epsilon) StartCooldown(); // CD only starts ticking after cast time finishes
            else UsageTween = Tween.Delay(cachedUsageTime, StartCooldown);

            return true;
        }

        public void OnHit(IDamageable target) {
            foreach (var item in actions) {
                item.OnHit(owner, this, target);
            }
            foreach (var item in modifiers) {
                item.OnHit(owner, this, target);
            }
        }

        public float CalculateMPCost(AbilityManager owner) {
            float mp = 0;
            foreach (var item in actions) {
                mp += item.BaseCost;
            }
            cachedMPCost = mp; // TODO: cost reduction?

            float costr = owner.Attributes.MPCostReduction;
            foreach (var item in modifiers) {
                item.definition.PerAbilityModifier.ForEach(x => {
                    if (x.Attribute == GameAttributes.MPCostReduction)
                        costr += x.value.GetValueAtLevel(item.level);
                });
            }
            cachedMPCost = Formulas.CostReductionFormula(cachedMPCost, costr);

            return cachedMPCost;
        }

        public float CalculateCooldownTime(AbilityManager owner) {
            float cooldown = 0;
            //* Not going to bother with action-level cooldown reduction.
            //* CDR can either affect the player (CDR stat) or the ability only. 
            foreach (var item in actions) {
                cooldown += item.BaseCooldown;
            }
            float cdr = owner.Attributes.CooldownReduction;
            foreach (var item in modifiers) {
                item.definition.PerAbilityModifier.ForEach(x => {
                    if (x.Attribute == GameAttributes.CooldownReduction)
                        cdr += x.value.GetValueAtLevel(item.level);
                });
            }

            cachedCooldownTime = Formulas.CooldownReductionFormula(cooldown, cdr);
            CooldownDisplay = cachedCooldownTime;
            return cachedCooldownTime;
        }

        public virtual float CalculateUsageTime(AbilityManager owner) {
            float usage = 0;
            foreach (var item in actions) {
                if (item == null) break;
                if (item.definition.actionType == ActionType.BasicAttack) {
                    float attackTime = item.definition.UsageTime;
                    float attackSpeed = owner.Attributes.AttackSpeed;
                    attackTime = Formulas.AttackSpeedFormula(attackTime, attackSpeed);
                    usage = Mathf.Max(usage, attackTime);
                    continue;
                }
                usage = Mathf.Max(usage, item.definition.UsageTime);
            }
            cachedUsageTime = usage;
            UsageTimeDisplay = cachedUsageTime;
            return cachedUsageTime;
        }


        private void StartCooldown() {
            OnCooldownStarted?.Invoke(cachedCooldownTime);
            if (cachedCooldownTime > float.Epsilon) CooldownTween = Tween.Delay(cachedCooldownTime, OnCooldownEnd);
            else OnCooldownEnd();

        }
        private void OnCooldownEnd() {
            isOnCooldown = false;
            OnCooldownEnded?.Invoke();
        }


        public AddStatus CanSwapModifier(ModifierInstance incoming, int index) {
            if (incoming == null) return AddStatus.Null;
            if (index >= modifiers.Count) {
                // just check for duplicates
                if (modifiers.Any(x => x.definition == incoming.definition)) return AddStatus.DuplicateOnSameAbility;
                return AddStatus.Available;
            }
            var target = modifiers[index];
            if (incoming.definition == target.definition) {
                if (incoming.level >= target.level) return AddStatus.Available;
                else return AddStatus.MergeTargetHasHigherLevel;
            }

            if (modifiers.Any(x => x.definition == incoming.definition)) return AddStatus.DuplicateOnSameAbility;
            return AddStatus.Available;
        }

        public AddStatus CanSwapAction(ActionInstance incoming, int index) {
            if (incoming == null) return AddStatus.Null;
            if (index >= actions.Count) {
                // just check for duplicates
                if (actions.Any(x => x.definition == incoming.definition)) return AddStatus.DuplicateOnSameAbility;
                return AddStatus.Available;
            }
            var target = actions[index];
            if (incoming.definition == target.definition) {
                if (incoming.level >= target.level) return AddStatus.Available;
                else return AddStatus.MergeTargetHasHigherLevel;
            }

            if (actions.Any(x => x.definition == incoming.definition)) return AddStatus.DuplicateOnSameAbility;
            return AddStatus.Available;
        }

        public void SwapModifier(ModifierInstance incoming, int index) {
            // We assume that this has already been validated
            if (index >= modifiers.Count) {
                modifiers.Add(incoming);
                return;
            }
            var target = modifiers[index];
            if (target.definition == incoming.definition) {
                if (target.level == incoming.level) target.level++;
                else target.level = Mathf.Max(target.level, incoming.level);
            } else {
                modifiers[index] = incoming;
            }
        }
        public void SwapAction(ActionInstance incoming, int index) {
            // We assume that this has already been validated
            if (index >= actions.Count) {
                actions.Add(incoming);
                return;
            }
            var target = actions[index];
            // TODO: Fix the null ref exception here
            if (target.definition == incoming.definition) {
                if (target.level == incoming.level) target.level++;
                else target.level = Mathf.Max(target.level, incoming.level);
            } else {
                actions[index] = incoming;
            }
        }

        /// <summary>
        /// Function that checks through all available actions, then sets the AnimatorStateInfo. 
        /// The priority number with the lowest value is taken. Ties are broken based on the slot order.
        /// </summary>
        public void RecalculateAnimation() {
            AnimationStateInfo animationToPlay = null;
            int priority = int.MaxValue;
            foreach (var item in actions) {
                var anim = item.definition.AnimationToPlay;
                if (!anim) continue;
                if (anim.priority < priority) {
                    animationToPlay = anim;
                }
            }
            StateToPlay = animationToPlay;
        }

        private string RegenerateDescription() {
            List<string> results = new();
            actions.ForEach(x => results.Add(x.definition.GetActionDescription(x.level)));
            // add modifiers?
            CachedDescription = string.Join("\n", results);
            return CachedDescription;
        }

        public string GetDescription() {
            if (CachedDescription.Length != 0) return CachedDescription;
            return RegenerateDescription();
        }
    }
}