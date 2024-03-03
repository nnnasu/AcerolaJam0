using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PrimeTween;
using UnityEngine;

namespace Core.Abilities.Instances {
    [Serializable]
    public class AbilityInstance {

        public AbilityManager owner;
        public List<ActionInstance> actions = new(4);
        public List<ModifierInstance> modifiers = new(4);
        public float CooldownDisplay;
        public bool isOnCooldown { get; private set; } = false;
        public float cachedCooldownTime { get; private set; }
        public float cachedUsageTime { get; private set; }
        public bool useDynamicCooldown = false;
        public Tween CooldownTween; // sum(action cd)
        public Tween UsageTween; // cast time = max(action cast time)

        public event Action OnAbilityActivated = delegate { };
        public event Action<float> OnCooldownStarted = delegate { };
        public event Action OnCooldownEnded = delegate { };
        public event Action<bool> OnFocusChanged = delegate { };

        public AbilityInstance(AbilityManager owner) {
            this.owner = owner;
        }

        public void OnAbilityModified() {
            CalculateCooldownTime(owner);
            CalculateUsageTime(owner);
        }

        public bool SetFocus(bool focused) {
            // TODO FIX THIS
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

            foreach (var item in actions) {
                item.ActivateAbility(owner, this, targetPoint);
            }

            foreach (var item in modifiers) {
                item.OnActivate(owner, this, targetPoint);
            }
            // Start Cooldowns
            isOnCooldown = true;
            OnAbilityActivated?.Invoke();
            Debug.Log($"Current CD = {cachedCooldownTime}");
            StartCooldown();
            // if (cachedUsageTime <= float.Epsilon) StartCooldown(); // CD only starts ticking after cast time finishes
            // else UsageTween = Tween.Delay(cachedUsageTime, StartCooldown);

            return true;
        }

        public void OnHit(AttributeSet target) {
            foreach (var item in actions) {
                item.OnHit(owner, this, target);
            }
            foreach (var item in modifiers) {
                item.OnHit(owner, this, target);
            }
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
                    if (x.Attribute == Attributes.CooldownReduction)
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
                usage = Mathf.Max(usage, item.definition.UsageTime);
            }
            cachedUsageTime = usage;

            return cachedUsageTime;
        }


        private void StartCooldown() {
            if (cachedCooldownTime >= float.Epsilon) {
                OnCooldownStarted?.Invoke(cachedCooldownTime);
                CooldownTween = Tween.Delay(cachedCooldownTime, OnCooldownEnd);
            } else {
                OnCooldownEnd();
            }
        }
        private void OnCooldownEnd() {
            isOnCooldown = false;
            OnCooldownEnded?.Invoke();
        }


    }
}