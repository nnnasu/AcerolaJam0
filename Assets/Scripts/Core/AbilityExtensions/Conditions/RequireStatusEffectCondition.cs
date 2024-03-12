using System;
using System.Collections;
using System.Collections.Generic;
using Core.AttributeSystem.Conditions;
using Core.Abilities.Instances;
using UnityEngine;
using Core.AttributeSystem.Alignments;

namespace Core.AbilityExtensions.Conditions {

    [CreateAssetMenu(fileName = "RequireStatus", menuName = "Ability System/Status Effect Requirements/Require Status Effect", order = 0)]
    public class RequireStatusEffectCondition : TargetCondition {

        public StatusEffect effectRequired;

        public override bool TestCondition(AttributeSet target) {
            if (target is PlayerAttributeSet playerAttributeSet) {
                return playerAttributeSet.ActiveEffects.ContainsKey(effectRequired);
            }
            return false;
        }
    }

}