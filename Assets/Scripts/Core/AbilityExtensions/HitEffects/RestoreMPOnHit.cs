using Core.Abilities;
using Core.Abilities.Effects;
using Core.Abilities.Instances;
using Core.Utilities.Scaling;
using UnityEngine;

namespace Core.AbilityExtensions.Effects {
    [CreateAssetMenu(fileName = "MPRegenEffect", menuName = "Ability System/On Hit Effects/MP Regen On Hit", order = 0)]
    public class RestoreMPOnHit : OnHitEffect {
        public ScaledFloat RegenOnHit;

        public override string GetTooltip(int level) {
            return $"Recovers {RegenOnHit.GetValueAtLevel(level)} MP on hit.";
        }

        public override void OnHit(AbilityManager owner, AbilityInstance ability, ActionInstance action, AttributeSet target) {
            float amount = RegenOnHit.GetValueAtLevel(action.level);
            owner.Attributes.CostMana(-amount); // negative negative            
        }
    }
}