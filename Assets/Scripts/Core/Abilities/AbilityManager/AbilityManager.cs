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

        public float movementSpeed => Attributes.MovementSpeedCurrent;

        private int index = 0;
        public bool isSkillSelected => index != 0;

        private void Awake() {
            for (int i = 0; i < 4; i++) {
                Abilities.Add(new(this));
            }
            BasicAttack = new(this);
        }

        public void SetActiveSkill(int index) {
            Abilities.ForEach(x => x.SetFocus(false)); // clear current selected
            if (index == 0) {
                this.index = 0;
                return;
            }

            int abilityIndex = index - 1;
            if (abilityIndex < Abilities.Count) {
                if (!Abilities[abilityIndex].SetFocus(true)) {
                    SetActiveSkill(0); // Reset to basic attack if we couldn't set focus.
                } else {
                    this.index = index;
                }
            }
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

        }

        public void RecalculateStats() {
            BasicAttack.OnAbilityModified();
            Abilities.ForEach(x => x.OnAbilityModified());

            OnRebindRequest?.Invoke();

        }
    }
}