

using System;
using Core.Utilities.Scaling;

namespace Core.Abilities.Definitions {
    [Serializable]
    public class StatModifier {
        public Attributes Attribute;
        public ScaledFloat value;

        public string GetTooltipText(float level, bool perAbility = false) {
            float change = value.GetValueAtLevel(level);

            if (perAbility) return $"{Attribute} {(change >= 0 ? "+" : "")}{change} for this ability only.";
            return $"{Attribute} {(change >= 0 ? "+" : "")}{change}";
        }

    }
}