using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using Core.Abilities.Effects;
using Core.Abilities.Instances;
using UnityEngine;

namespace Core.AbilityExtensions.ActivationEffects {
    [CreateAssetMenu(fileName = "RemoveStatusEffectOnActivation", menuName = "Ability System/On Activate Effects/Remove Status Effect", order = 0)]
    public class RemoveStatusEffectOnActivate : OnActivateEffect {

        public StatusEffect EffectToApply;
        [TextArea] public string Description;

        public override string GetTooltip(int level) {
            return $"Removes {EffectToApply.name} on activation";
        }

        public override void OnActivateImpl(AbilityManager owner, AbilityInstance ability, ActionInstance action, Action<AttributeSet> OnHit = null) {
            owner.Attributes.RemoveEffect(EffectToApply);
        }

    }
}