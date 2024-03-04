using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Core.Utilities.Scaling {
    public enum ScalingType {
        [Tooltip("Linear scaling. Value = Base * Level + Additive")]
        Linear,

        [Tooltip("Curve-based scaling. Value = f(Base, level) + Additive")]
        Custom,

        [Tooltip("Constant scaling. Value = Base + Additive")]
        Constant
    }

    [Serializable]
    public class ScaledFloat {


        public ScalingType scalingType = ScalingType.Linear;
        public float BaseValue;
        public float AdditiveValue;
        public AnimationCurve ValueCurve = new(new Keyframe(0, 1), new Keyframe(5, 2)) {
            preWrapMode = WrapMode.ClampForever,
            postWrapMode = WrapMode.ClampForever,
        };

        public float GetValueAtLevel(int level) {
            return scalingType switch {
                ScalingType.Linear => BaseValue * level + AdditiveValue,
                ScalingType.Custom => ValueCurve.Evaluate(level) * BaseValue + AdditiveValue,
                ScalingType.Constant => BaseValue + AdditiveValue,
                _ => 0
            };
        }

    }
}