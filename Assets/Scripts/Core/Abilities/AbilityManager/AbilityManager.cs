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
            BasicAttack.OnAbilityModified();
            Abilities.ForEach(x => x.OnAbilityModified());

            OnRebindRequest?.Invoke();
        }

        private void ApplyModifier(ModifierInstance modifier) {
            modifier.definition.GlobalStatModifier.ForEach(x => {
                
            });
        }
    }
}