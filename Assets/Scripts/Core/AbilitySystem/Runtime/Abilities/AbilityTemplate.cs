using System.Collections;
using System.Collections.Generic;
using Core.AbilitySystem.Alignments;
using UnityEngine;

public enum AbilityType {
    BasicAttack,
    Spell,
    Structure
}

namespace Core.AbilitySystem.Abilities {
    [CreateAssetMenu(fileName = "AbilityTemplate", menuName = "Abilities/AbilityTemplate", order = 0)]
    public class AbilityTemplate : ScriptableObject {
        public TargetingType TargetType;
        public AberrantType Alignment;
        public AbilityType AbilityType;

        [SerializeReference]
        public List<AbilityAction> Actions;

        public float[] DamageScaling = new float[] { 1 };



    }
}