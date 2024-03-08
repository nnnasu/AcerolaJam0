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
        public CharacterController characterController;
        public Vector3 previousPosition; // before applying abilities
        public event Action<PlayerAttributeSet> OnAlignmentRecalculated = delegate { };


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
            previousPosition = transform.position;
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


        public void RecalculateAlignmentLevels() {
            Attributes.levels.Clear();
            BasicAttack.actions.ForEach(y => {
                if (!Attributes.levels.ContainsKey(y.definition.alignment)) Attributes.levels.Add(y.definition.alignment, 0);
                Attributes.levels[y.definition.alignment] += y.level;
            });
            BasicAttack.modifiers.ForEach(y => {
                if (!Attributes.levels.ContainsKey(y.definition.alignment)) Attributes.levels.Add(y.definition.alignment, 0);
                Attributes.levels[y.definition.alignment] += y.level;
            });
            Abilities.ForEach(x => {
                x.actions.ForEach(y => {
                    if (!Attributes.levels.ContainsKey(y.definition.alignment)) Attributes.levels.Add(y.definition.alignment, 0);
                    Attributes.levels[y.definition.alignment] += y.level;
                });
                x.modifiers.ForEach(y => {
                    if (!Attributes.levels.ContainsKey(y.definition.alignment)) Attributes.levels.Add(y.definition.alignment, 0);
                    Attributes.levels[y.definition.alignment] += y.level;
                });
            });
            OnAlignmentRecalculated?.Invoke(Attributes);
        }

        public void RecalculateStats() {
            // Apply stat modifiers.
            float oldMaxHP = Attributes.MaxHP;
            float oldMaxMP = Attributes.MaxMP;
            Attributes.ResetState();
            BasicAttack.modifiers.ForEach(x => ApplyGlobalModifiers(x));
            Abilities.ForEach(x => x.modifiers.ForEach(x => ApplyGlobalModifiers(x)));
            // TODO: Fix this from adding more MP modifiers every reward

            Attributes.HP += Mathf.Clamp(Attributes.MaxHP - oldMaxHP, 0, float.MaxValue);
            Attributes.MP += Mathf.Clamp(Attributes.MaxMP - oldMaxMP, 0, float.MaxValue);

            BasicAttack.OnAbilityModified();
            Abilities.ForEach(x => x.OnAbilityModified());

            OnRebindRequest?.Invoke();
        }

        private void ApplyGlobalModifiers(ModifierInstance modifier) {
            modifier.definition.GlobalStatModifier.ForEach(x => {
                Attributes.ApplyModifier(x, modifier.level);
            });
        }

        public void Teleport(Vector3 position) {
            characterController.enabled = false;
            transform.position = position;
            characterController.enabled = true;
        }
    }
}