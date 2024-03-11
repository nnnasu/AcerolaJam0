using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.AttributeSystem.Conditions {

    public abstract class TargetCondition : ScriptableObject {
        public abstract bool TestCondition(AttributeSet target);

    }
}