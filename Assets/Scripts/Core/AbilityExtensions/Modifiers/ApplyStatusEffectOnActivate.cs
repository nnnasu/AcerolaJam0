using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using Core.Abilities.Instances;
using UnityEngine;

namespace Core.AbilityExtensions.Modifiers {
    [CreateAssetMenu(fileName = "StatusEffectActivation", menuName = "Ability System/Ability Modifiers/Status Effect on Activation", order = 0)]    
    public class ApplyStatusEffectOnActivate : AbilityModifierDefinition {
        public StatusEffect effectToApply;
        
        public override void OnActivate(AbilityManager owner, AbilityInstance ability, Vector3 target, ModifierInstance mod, Action<AttributeSet> OnHit = null) {
            var instance = effectToApply.GetEffectInstance(owner.Attributes, mod.level);
            owner.Attributes.ApplyEffect(instance);
        }

        public override void OnHit(AbilityManager owner, AbilityInstance ability, ModifierInstance mod, AttributeSet hitTarget) {
        }
    }
}