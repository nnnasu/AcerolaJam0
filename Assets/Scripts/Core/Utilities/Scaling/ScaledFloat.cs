using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Core.Utilities.Scaling {
    [Serializable]
    public class ScaledFloat {
        public float BaseValue;
        public float AdditiveValue;
        public AnimationCurve ValueCurve = new(new Keyframe(0, 1), new Keyframe(5, 2)) {
            preWrapMode = WrapMode.ClampForever,
            postWrapMode = WrapMode.ClampForever,            
        };

        public float GetValueAtLevel(float level) {
            return ValueCurve.Evaluate(level) * BaseValue + AdditiveValue;
        }

    }
}