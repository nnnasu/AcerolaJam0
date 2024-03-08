using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities.Conditions;
using Core.Abilities.Instances;
using UnityEngine;

namespace Core.AbilityExtensions.Conditions {

    public class AlignmentCondition : TargetCondition {

        public override bool TestCondition(AttributeSet target) {
            return true;
        }
    }
}