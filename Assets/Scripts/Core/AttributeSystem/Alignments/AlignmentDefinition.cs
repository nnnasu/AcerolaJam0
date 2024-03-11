using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.AttributeSystem.Alignments {

    [Serializable]
    public class EffectLevelPair {
        public int level;
        public StatusEffect effect;
    }

    [CreateAssetMenu(fileName = "AlignmentDefinition", menuName = "Ability System/Alignments/Alignment Definition", order = 0)]
    public class AlignmentDefinition : ScriptableObject {

        public string AlignmentName; // used for tooltips?

        [Tooltip("If false, only the highest effect is applied. Otherwise, all effects listed will be applied.")]
        public bool ApplyHighestEffectOnly = false;

        public List<EffectLevelPair> Effects = new();

        private void OnValidate() {
            Effects.Sort((x, y) => x.level.CompareTo(y.level));
        }

        public virtual void ApplyEffects(PlayerAttributeSet player, int level) {
            RemoveEffects(player, level);
            if (ApplyHighestEffectOnly) ApplyHighestEffect(player, level);
            else ApplyAllEffects(player, level);
        }

        protected virtual void ApplyHighestEffect(PlayerAttributeSet player, int level) {
            StatusEffect highest = null;
            foreach (var item in Effects) {
                if (item.level > level) break;
                // OnValidate ensures that this is sorted.
                highest = item.effect;
            }
            if (highest == null) return;

            ApplySingleEffect(highest, player, level);
        }

        protected virtual void ApplyAllEffects(PlayerAttributeSet player, int level) {
            Effects
                .Where(x => x.level <= level)
                .ToList()
                .ForEach(x => ApplySingleEffect(x.effect, player, level));
        }

        protected virtual void ApplySingleEffect(StatusEffect effect, PlayerAttributeSet player, int level) {
            // Effects can scale with alignment level            
            var instance = effect.GetEffectInstance(player, level);
            player.ApplyEffect(instance);
        }

        public virtual void RemoveEffects(PlayerAttributeSet player, int level) {
            foreach (var item in Effects) {
                if (player.ActiveEffects.ContainsKey(item.effect)) {
                    player.RemoveEffect(item.effect);
                }
            }
        }

        public string GetTooltipText(int level) {
            return $"{AlignmentName} +{level}";
        }




    }
}