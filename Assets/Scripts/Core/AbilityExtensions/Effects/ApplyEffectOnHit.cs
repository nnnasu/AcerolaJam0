using Core.Abilities;
using Core.Abilities.Effects;
using Core.Abilities.Instances;
using Core.Utilities.Scaling;
using UnityEngine;

namespace Core.AbilityExtensions.Effects {
    [CreateAssetMenu(fileName = "DebuffEffect", menuName = "Effects/On Hit Effects/Debuff On Hit", order = 0)]
    public class ApplyEffectOnHit : OnHitEffect {
        public GameplayEffect effect;

        public override string GetTooltip(ActionInstance action) {
            return $"Applies {effect.name} on hit.";
        }

        public override void OnHit(AbilityManager owner, AbilityInstance ability, ActionInstance action, AttributeSet target) {
            target.ApplyEffect(new(effect, action.level));
        }
    }
}