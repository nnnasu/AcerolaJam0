using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using Core.Abilities.Instances;
using UnityEngine;

namespace Core.AbilityExtensions.Modifiers {
    [CreateAssetMenu(fileName = "SimpleModifier", menuName = "Modifiers/Simple", order = 0)]
    
    public class SimpleModifier : ModifierDefinition {
        public override void OnActivate(AbilityManager owner, AbilityInstance ability, Vector3 target, ModifierInstance mod, Action<AttributeSet> OnHit = null) {
        }

        public override void OnHit(AbilityManager owner, AbilityInstance ability, ModifierInstance mod, AttributeSet hitTarget) {
        }
    }
}