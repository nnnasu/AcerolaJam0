using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Utilities.Scaling {

    public enum FormulaType {

        [InspectorName("level + extra")]
        SpawnFormula,
        [InspectorName("(ln(level + extra))^exponent")]
        CreditFormula
    }

    [CreateAssetMenu(fileName = "ScalingFormula", menuName = "Rooms/ScalingFormula", order = 0)]
    public class ScalingFormula : ScriptableObject {

        public FormulaType formulaType;
        public bool ClampMin = false;
        public bool ClampMax = false;
        public float MinValue = 0;
        public float MaxValue = 100;

        public int ExtraLevel = 0;
        public float Exponents = 1;

        public float GetValue(int level) {
            float value = formulaType switch {
                FormulaType.SpawnFormula => Formulas.SpawnFormula(level, 0),
                FormulaType.CreditFormula => Formulas.CreditFormula(level, ExtraLevel, Exponents),
                _ => 0
            };

            if (ClampMax) value = Mathf.Min(value, MaxValue);
            if (ClampMin) value = Mathf.Max(value, MinValue);
            return value;
        }

        public int GetFlooredValue(int level) {
            return Mathf.FloorToInt(GetValue(level));
        }




    }
}