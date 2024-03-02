using System;
using Core.Abilities.Definitions;
using UnityEngine;


namespace Core.Abilities.Instances {
    [Serializable]
    public class StructureStorageInstance {
        public StructureDefinition structure;
        public int MaxCount;
        public int ActiveStructures;
        public int CurrentCharges;



    }
}