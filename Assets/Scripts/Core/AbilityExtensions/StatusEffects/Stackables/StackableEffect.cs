using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.AttributeSystem.Alignments;
using UnityEngine;

namespace Core.AbilityExtensions.StatusEffects.Stackables {
    [CreateAssetMenu(fileName = "StackableEffect", menuName = "Ability System/Status Effects/Stackable", order = 0)]
    public class StackableEffect : StatusEffect {
        public List<StatModifier> modifiers = new();
        public bool UseAlignmentLevelForScaling = false;
        public AlignmentDefinition alignmentToScaleWith;


        public override EffectInstance GetEffectInstance(AttributeSet target, int level) {

            if (UseAlignmentLevelForScaling) {
                if (target is PlayerAttributeSet player) {
                    if (player.levels.ContainsKey(alignmentToScaleWith)) level = player.levels[alignmentToScaleWith];
                }
            }
            
            if (target.ActiveEffects.ContainsKey(this) && target.ActiveEffects[this] is StackableEffectInstance current) {
                return new StackableEffectInstance(this, level, current.stackCount + 1);
            }
            return new StackableEffectInstance(this, level, 1);
        }

        public override void Apply(AttributeSet attributeSet, EffectInstance instance) {
            int stacks = 1;
            if (instance is StackableEffectInstance stackableEffectInstance) stacks = stackableEffectInstance.stackCount;
            foreach (var item in modifiers) {
                attributeSet.ApplyModifier(item, instance.level, false, stacks);
            }
        }

        public override string GetDescription(EffectInstance instance) {
            if (instance is StackableEffectInstance stackable) {
                var strings = modifiers.Select(x => (x.Attribute, x.value.GetValueAtLevel(stackable.level) * stackable.stackCount))
                    .Select(x => $"{x.Attribute} + {x.Item2}")
                    .ToList();

                return string.Join("\n", strings);
            }
            return "Error";
        }

        public override void Remove(AttributeSet attributeSet, EffectInstance instance) {
            int stacks = 1;
            if (instance is StackableEffectInstance stackableEffectInstance) stacks = stackableEffectInstance.stackCount;
            foreach (var item in modifiers) {
                attributeSet.ApplyModifier(item, instance.level, true, stacks);
            }
        }

    }
}