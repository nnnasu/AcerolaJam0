using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities.Instances;
using UnityEngine;

namespace Core.Abilities.Conditions {

    public abstract class TargetCondition : ScriptableObject {
        public abstract bool TestCondition(AttributeSet target);

    }
}