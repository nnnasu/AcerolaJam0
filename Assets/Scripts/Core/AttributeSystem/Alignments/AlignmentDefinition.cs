using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.AttributeSystem.Alignments {

    [Serializable]
    public class EffectLevelPair {
        public int level;
        public StatusEffect effect;
    }

    [CreateAssetMenu(fileName = "AlignmentDefinition", menuName = "Ability System/Alignments/Alignment Definition", order = 0)]
    public class AlignmentDefinition : ScriptableObject {

        public string AlignmentName; // used for tooltips?

        [Tooltip("If false, only the highest effect is applied. Otherwise, all effects listed will be applied.")]
        public bool EffectsStack = true;

        public List<EffectLevelPair> Effects = new();

        private void OnValidate() {
            Effects.Sort((x, y) => x.level.CompareTo(y.level));
        }



    }
}