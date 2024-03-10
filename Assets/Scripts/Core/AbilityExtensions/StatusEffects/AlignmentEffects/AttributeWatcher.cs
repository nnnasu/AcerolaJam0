using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.AbilityExtensions.StatusEffects.AlignmentEffects {
    public enum WatchableAttributes {
        HP,
        MP
    }



    [CreateAssetMenu(fileName = "AttributeWatcher", menuName = "Ability System/Status Effects/AttributeWatcher", order = 0)]
    public class AttributeWatcher : StatusEffect {

        [TextArea] public string Description;

        public WatchableAttributes watchedAttribute = WatchableAttributes.HP;
        [Range(0, 1)] public float threshold;
        public bool effectTriggersAboveThreshold = false;
        public bool Reversible = true;
        public List<StatusEffect> effectsToTrigger = new();


        public override EffectInstance GetEffectInstance(AttributeSet target, int level) {
            return new WatcherInstance(this, level);
        }

        public override void Apply(AttributeSet attributeSet, EffectInstance instance) {
            WatcherInstance watcher = instance as WatcherInstance;
            if (watcher == null) return;
        }

        public override string GetDescription(EffectInstance instance) {
            WatcherInstance watcher = instance as WatcherInstance;
            string preamble = $"Watches {watchedAttribute}. When {(effectTriggersAboveThreshold ? "above" : "below")} {(threshold * 100).ToString("0")}%, triggers the following effects:\n";
            var strings = effectsToTrigger.Select(x => x.GetDescription(instance)).ToList();
            return preamble + string.Join("\n", strings);
        }

        public override void Remove(AttributeSet attributeSet, EffectInstance instance) {
            WatcherInstance watcher = instance as WatcherInstance;
            if (watcher == null) return;
        }
    }
}