using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities.Definitions;
using Core.Abilities.Instances;
using Core.Abilities.Structures;
using UnityEngine;

namespace Core.Abilities {
    public class AbilityManager : MonoBehaviour {
        public PlayerAttributeSet Attributes;
        public Dictionary<StructureDefinition, StructureStorageInstance> StructureStorage = new();
        public event Action OnRebindRequest = delegate { };

        public AbilityInstance BasicAttack;
        public List<AbilityInstance> Abilities = new(4);

        public float movementSpeed => Attributes.MovementSpeed;
        public ActionDefinition DefaultAttack;

        private int index = 0;
        public bool isSkillSelected => index != 0;

        public AbilityInstance CurrentlySelected { get; private set; }
        public event Action<AbilityInstance> OnCurrentSelectedAbilityChanged = delegate { };


        private void Awake() {
            Initialize();
            CurrentlySelected = BasicAttack;
        }

        public void Initialize() {
            // TODO: Reset to base stats
            Abilities.Clear();

            for (int i = 0; i < 4; i++) {
                Abilities.Add(new(this));
            }
            BasicAttack = new(this);
            if (DefaultAttack) BasicAttack.actions.Add(new(DefaultAttack));
            Attributes.ResetState(true);
            RecalculateStats();
        }

        public void SetActiveSkill(int index) {
            Abilities.ForEach(x => x.SetFocus(false)); // clear current selected
            if (index == 0) {
                this.index = 0;
                CurrentlySelected = BasicAttack;
                OnCurrentSelectedAbilityChanged?.Invoke(CurrentlySelected);
                return;
            }

            int abilityIndex = index - 1;
            if (abilityIndex < Abilities.Count) {
                if (!Abilities[abilityIndex].SetFocus(true)) {
                    SetActiveSkill(0); // Reset to basic attack if we couldn't set focus.
                } else {
                    this.index = index;
                    CurrentlySelected = Abilities[abilityIndex];
                }
            }
            OnCurrentSelectedAbilityChanged?.Invoke(CurrentlySelected);
        }

        public void OnClick(Vector3 targetPosition) {
            if (index == 0) {
                BasicAttack?.ActivateAbility(targetPosition);
            } else {
                if (index - 1 >= Abilities.Count) return;

                var ability = Abilities[index - 1];
                ability?.ActivateAbility(targetPosition);
            }
            SetActiveSkill(0);
        }

        public void OnStructureRecall(StructureBase structureBase) {
            structureBase.OnRecall(this);
            structureBase.ReturnToPool();
        }

        [ContextMenu("Recalculate Stats")]
        public void RecalculateStats() {
            // Apply stat modifiers.
            float oldMaxHP = Attributes.MaxHP;
            float oldMaxMP = Attributes.MaxMP;
            Attributes.ResetState();
            BasicAttack.modifiers.ForEach(x => ApplyGlobalModifiers(x));
            Abilities.ForEach(x => x.modifiers.ForEach(x => ApplyGlobalModifiers(x)));

            Debug.Log($"{Attributes.MaxHP - oldMaxHP} + {Attributes.HP}");

            Attributes.HP += Mathf.Clamp(Attributes.MaxHP - oldMaxHP, 0, float.MaxValue);
            Attributes.MP += Mathf.Clamp(Attributes.MaxMP - oldMaxMP, 0, float.MaxValue);

            BasicAttack.OnAbilityModified();
            Abilities.ForEach(x => x.OnAbilityModified());

            OnRebindRequest?.Invoke();
        }

        private void ApplyGlobalModifiers(ModifierInstance modifier) {
            modifier.definition.GlobalStatModifier.ForEach(x => {
                float value = x.value.GetValueAtLevel(modifier.level);
                switch (x.Attribute) {
                    case GameAttributes.MaxHP:
                        Attributes.MaxHP += value;
                        break;
                    case GameAttributes.MaxMP:
                        Attributes.MaxMP += value;
                        break;
                    case GameAttributes.MovementSpeed:
                        Attributes.MovementSpeed += value;
                        break;
                    case GameAttributes.AttackSpeed:
                        Attributes.AttackSpeed += value;
                        break;
                    case GameAttributes.BaseAttack:
                        Attributes.BaseAttack += value;
                        break;
                    case GameAttributes.MPRegenPercent:
                        Attributes.MPRegenPercent += value;
                        break;
                    case GameAttributes.HPRegenPercent:
                        Attributes.HPRegenPercent += value;
                        break;
                    case GameAttributes.StructureTickSpeed:
                        Attributes.StructureTickSpeed += value;
                        break;
                    case GameAttributes.CooldownReduction:
                        Attributes.CooldownReduction += value;
                        break;
                    default: break;
                }

            });
        }
    }
}