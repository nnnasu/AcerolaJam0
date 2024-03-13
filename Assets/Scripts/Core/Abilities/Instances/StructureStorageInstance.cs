using System;
using System.Collections.Generic;
using Core.Abilities.Definitions;
using Core.Abilities.Structures;
using PrimeTween;
using UnityEngine;


namespace Core.Abilities.Instances {
    [Serializable]
    public class StructureStorageInstance {
        public AbilityManager owner;
        public StructureDefinition structure;
        public int MaxCount;
        public int CurrentCharges;
        public int TotalLevel; // Used for count
        public int numberOfAbilities;
        public float AverageLevel; // if duplicates are present
        public LinkedList<StructureBase> ActiveStructures = new(); // Use to clean up structures when leaving a level.

        public StructureStorageInstance(AbilityManager owner, StructureDefinition definition) {
            this.owner = owner;
            structure = definition;
        }

        public void Remove(StructureBase structure) {
            ActiveStructures.Remove(structure);
        }

        public StructureBase GetStructure() {
            if (ActiveStructures.Count == MaxCount) {
                // Remove the oldest without executing recall
                owner.OnStructureRecall(ActiveStructures.First.Value, false);
            }
            var obj = structure.SpawnStructure();
            var stru = obj.GetComponent<StructureBase>();
            ActiveStructures.AddLast(stru);
            return stru;
        }


    }
}