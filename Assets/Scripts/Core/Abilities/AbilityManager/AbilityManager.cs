using System.Collections;
using System.Collections.Generic;
using Core.Abilities.Definitions;
using Core.Abilities.Instances;
using UnityEngine;

namespace Core.Abilities {
    public class AbilityManager : MonoBehaviour {
        public PlayerAttributeSet Attributes;
        public Dictionary<StructureDefinition, StructureStorageInstance> StructureStorage = new();

        public AbilityInstance BasicAttack;
        public List<AbilityInstance> Abilities = new(4);


        public void OnClick(Vector3 targetPosition, int selectedKey = 0) {
            if (selectedKey == 0) {
                BasicAttack?.ActivateAbility(targetPosition);
            } else {
                if (selectedKey >= Abilities.Count) return;

                var ability = Abilities[selectedKey - 1];
                ability?.ActivateAbility(targetPosition);
            }

        }

        public void RecalculateStats() {

        }
    }
}