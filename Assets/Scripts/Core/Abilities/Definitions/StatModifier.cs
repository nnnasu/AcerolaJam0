

using System;
using Core.Utilities.Scaling;

namespace Core.Abilities.Definitions {
    [Serializable]
    public class StatModifier {
        public Attributes Attribute;
        public ScaledFloat value;
    }
}