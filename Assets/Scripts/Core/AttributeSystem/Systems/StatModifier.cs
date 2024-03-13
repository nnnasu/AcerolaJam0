

using System;
using Core.Utilities.Scaling;
using UnityEngine;

[Serializable]
public class StatModifier {
    public GameAttributes Attribute;
    public ScaledFloat value;

    public string GetTooltipText(int level, bool perAbility = false) {
        float change = value.GetValueAtLevel(level);

        if (perAbility) return $"{Attribute} {(change >= 0 ? "+" : "")}{change} for this ability.";
        return $"{Attribute} {(change >= 0 ? "+" : "")}{change}";
    }

    public float GetValue(int level, int stacks = 1) {
        if (Attribute == GameAttributes.DamageTaken || Attribute == GameAttributes.DamageDealt) {
            // i.e. 1.01, 1.02, 1.03, ...
            return Mathf.Min(1 + value.GetValueAtLevel(level) * stacks, float.Epsilon);
        }
        return stacks * value.GetValueAtLevel(level);
    }
}
