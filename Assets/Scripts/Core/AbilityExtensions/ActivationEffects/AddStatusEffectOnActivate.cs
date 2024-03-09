using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using Core.Abilities.Effects;
using Core.Abilities.Instances;
using UnityEngine;

namespace Core.AbilityExtensions.ActivationEffects {
    [CreateAssetMenu(fileName = "OnActivateStatusEffect", menuName = "Ability System/Effects/On Activate/Status Effect", order = 0)]
    public  class AddStatusEffectOnActivate : OnActivateEffect {

        public StatusEffect EffectToApply;
        [TextArea] public string Description;

        public override string GetTooltip(ActionInstance action) {
            return Description;
        }

        public override void OnActivate(AbilityManager owner, AbilityInstance ability, ActionInstance action, Action<AttributeSet> OnHit = null) {
            var instance = EffectToApply.GetEffectInstance(owner.Attributes, action.level);
            owner.Attributes.ApplyEffect(instance);
        }

    }
}