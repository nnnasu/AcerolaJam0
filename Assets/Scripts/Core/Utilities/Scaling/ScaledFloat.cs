using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Core.Utilities.Scaling {
    [Serializable]
    public class ScaledFloat {
        public float BaseValue;
        public float AdditiveValue;
        public AnimationCurve ValueCurve;

        public float GetValueAtLevel(float level) {
            return ValueCurve.Evaluate(level) * BaseValue + AdditiveValue;
        }
        
    }
}