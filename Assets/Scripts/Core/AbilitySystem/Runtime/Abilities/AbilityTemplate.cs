using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        [Header("Ability Information")]
        public TargetingType TargetType;
        public float TargetingWidth;
        public float TargetingDepth;
        public float TargetingHeight;

        public AberrantType Alignment;
        public AbilityType AbilityType;
        public float BaseCooldown = 1;


        [Header("Damage Scaling")]

        [Tooltip("Damage scaling as a multiplier of base attack. Time = level, value = multiplier, i.e. t=5 v=2 does 2xATK at level 5.")]
        public AnimationCurve DamageScalingCurve;
        public int MaximumLevel => (int)DamageScalingCurve.keys.Last().time;

        [Header("UI Properties")]
        public Texture2D icon;
        public string AbilityTitle;
        [TextArea] public string Description;

        [Header("Actions")]
        [SerializeReference] public List<AbilityAction> Actions;



    }
}