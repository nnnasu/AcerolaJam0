using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.AbilityExtensions.StatusEffects.AlignmentEffects {
    [CreateAssetMenu(fileName = "MageAlignment", menuName = "Effects/Alignment Effects/Mage", order = 0)]
    public class MagicAlignmentEffect : StatModificationEffect {

        public override void Apply(AttributeSet attributeSet, EffectInstance instance) {
            base.Apply(attributeSet, instance);
        }

        public override string GetDescription(EffectInstance instance) {
            return base.GetDescription(instance);
        }

        public override void Remove(AttributeSet attributeSet, EffectInstance instance) {
            base.Remove(attributeSet, instance);
        }

    }
}