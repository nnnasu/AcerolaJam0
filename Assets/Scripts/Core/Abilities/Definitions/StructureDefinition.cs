using System.Collections;
using System.Collections.Generic;
using Core.Abilities.Instances;
using UnityEngine;


namespace Core.Abilities.Definitions {


    [CreateAssetMenu(fileName = "StructureDefinition", menuName = "Ability System/Player Actions/Spawns/StructureDefinition", order = 0)]
    public class StructureDefinition : ScriptableObject {

        public int MinimumCapacity = 1;
        public int MaximumCapacity = 10;
        public float CapacityMultiplier; // Determines how this scales with total levels
        public GameObject structurePrefab;


        public GameObject SpawnStructure() {
            return  GlobalPool.Current.GetObject(structurePrefab);
        }

        public int GetMaxCount(float averageLevel) {
            return Mathf.Clamp(
                Mathf.FloorToInt(CapacityMultiplier * averageLevel),
                MinimumCapacity,
                MaximumCapacity
            );
        }


    }
}