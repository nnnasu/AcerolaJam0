using UnityEngine;
using PrimeTween;

namespace Core.AbilityExtensions.StatusEffects.AlignmentEffects {

    public class WatcherInstance : EffectInstance {

        private AttributeWatcher watcherDefinition;

        public WatcherInstance(StatusEffect def, int level) : base(def, level) {
            watcherDefinition = effectDefinition as AttributeWatcher;
        }

        public override void HandleEffectApplication(AttributeSet attributes) {
            base.HandleEffectApplication(attributes);


            switch (watcherDefinition.watchedAttribute) {
                case WatchableAttributes.HP:
                    attributes.OnHPChanged += OnAttributeChanged;
                    break;
                case WatchableAttributes.MP:
                    if (target is PlayerAttributeSet player) player.OnMPChanged += OnAttributeChanged;
                    break;

                default: break;
            }
        }

        public override void HandleEffectRemoval(AttributeSet attributes) {
            base.HandleEffectRemoval(attributes);
            switch (watcherDefinition.watchedAttribute) {
                case WatchableAttributes.HP:
                    attributes.OnHPChanged -= OnAttributeChanged;
                    break;
                case WatchableAttributes.MP:
                    if (target is PlayerAttributeSet player) player.OnMPChanged -= OnAttributeChanged;
                    break;

                default: break;
            }

        }

        public void OnAttributeChanged(float oldValue, float newValue) {
            bool shouldApply = false;
            switch (watcherDefinition.watchedAttribute) {
                case WatchableAttributes.HP:
                    shouldApply = TestThreshold(newValue, target.MaxHP);
                    break;

                case WatchableAttributes.MP:
                    if (target is PlayerAttributeSet player) shouldApply = TestThreshold(newValue, player.MaxMP);
                    break;
                default: break;
            }
            if (shouldApply) TriggerEffects();
            if (!shouldApply && watcherDefinition.Reversible) ReverseEffects();
        }

        private bool TestThreshold(float value, float maxValue) {
            float thresholdValue = maxValue * watcherDefinition.threshold; // e.g. 0.50 * 100 = 50, so check > or < 50
            if (watcherDefinition.effectTriggersAboveThreshold) return value >= thresholdValue;
            else return value < thresholdValue;
        }

        public void TriggerEffects() {

            watcherDefinition.effectsToTrigger.ForEach(x => {
                var instance = x.GetEffectInstance(target, level);
                target.ApplyEffect(instance);
            });
        }

        public void ReverseEffects() {
            watcherDefinition.effectsToTrigger.ForEach(x => {
                target.RemoveEffect(x);
            });
        }

    }
}