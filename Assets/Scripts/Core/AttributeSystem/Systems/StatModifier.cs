

using System;
using Core.Utilities.Scaling;

[Serializable]
public class StatModifier {
    public GameAttributes Attribute;
    public ScaledFloat value;    

    public string GetTooltipText(int level, bool perAbility = false) {
        float change = value.GetValueAtLevel(level);

        if (perAbility) return $"{Attribute} {(change >= 0 ? "+" : "")}{change} for this ability.";
        return $"{Attribute} {(change >= 0 ? "+" : "")}{change}";
    }
}
