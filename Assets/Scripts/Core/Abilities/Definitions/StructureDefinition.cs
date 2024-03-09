using System.Collections;
using System.Collections.Generic;
using Core.Abilities.Instances;
using UnityEngine;


namespace Core.Abilities.Definitions {


    [CreateAssetMenu(fileName = "StructureDefinition", menuName = "Ability System/Player Actions/Spawns/StructureDefinition", order = 0)]
    public class StructureDefinition : ScriptableObject {

        public float RechargeTime = 5;
        public GameObject structurePrefab;
        

        public GameObject SpawnStructure(Vector3 target, AbilityManager owner, AbilityInstance ability, ActionInstance action) {
            var obj = GlobalPool.Current.GetObject(structurePrefab);
            obj.transform.position = target;
            
            // Set any parameters on the actual structure object.
            return obj;
        }


    }
}