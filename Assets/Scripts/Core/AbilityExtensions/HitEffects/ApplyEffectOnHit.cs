using Core.Abilities;
using Core.Abilities.Effects;
using Core.Abilities.Instances;
using Core.Utilities.Scaling;
using UnityEngine;

namespace Core.AbilityExtensions.Effects {
    [CreateAssetMenu(fileName = "DebuffEffect", menuName = "Ability System/On Hit Effects/Debuff On Hit", order = 0)]
    public class ApplyEffectOnHit : OnHitEffect {
        public StatusEffect effect;

        public override string GetTooltip(ActionInstance action) {
            return $"Applies {effect.name} on hit.";
        }

        public override void OnHit(AbilityManager owner, AbilityInstance ability, ActionInstance action, AttributeSet target) {
            var instance = effect.GetEffectInstance(target, action.level);
            target.ApplyEffect(instance);
        }
    }
}