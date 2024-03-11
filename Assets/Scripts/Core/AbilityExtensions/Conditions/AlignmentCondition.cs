using System;
using System.Collections;
using System.Collections.Generic;
using Core.AttributeSystem.Conditions;
using Core.Abilities.Instances;
using UnityEngine;
using Core.AttributeSystem.Alignments;

namespace Core.AbilityExtensions.Conditions {

    [CreateAssetMenu(fileName = "AlignmentCondition", menuName = "Ability System/Status Effect Requirements/AlignmentCondition", order = 0)]
    public class AlignmentCondition : TargetCondition {

        public AlignmentDefinition Alignment;
        public int RequiredLevel = 10;

        public override bool TestCondition(AttributeSet target) {
            if (target is PlayerAttributeSet playerAttributeSet) {
                if (!playerAttributeSet.levels.ContainsKey(Alignment)) return false;

                return playerAttributeSet.levels[Alignment] >= RequiredLevel;
            }
            return false;
        }
    }

}