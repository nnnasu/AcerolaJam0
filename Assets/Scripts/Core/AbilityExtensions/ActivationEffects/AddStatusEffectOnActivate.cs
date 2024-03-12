using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using Core.Abilities.Effects;
using Core.Abilities.Instances;
using UnityEngine;

namespace Core.AbilityExtensions.ActivationEffects {
    [CreateAssetMenu(fileName = "AddStatusEffectOnActivation", menuName = "Ability System/On Activate Effects/Add Status Effect", order = 0)]
    public class AddStatusEffectOnActivate : OnActivateEffect {

        public StatusEffect EffectToApply;
        [TextArea] public string Description;

        public override string GetTooltip(int level) {
            return $"Applies {EffectToApply.name} on activation";
        }

        public override void OnActivateImpl(AbilityManager owner, AbilityInstance ability, ActionInstance action, Action<AttributeSet> OnHit = null) {
            var instance = EffectToApply.GetEffectInstance(owner.Attributes, action.level);
            owner.Attributes.ApplyEffect(instance);
        }

    }
}