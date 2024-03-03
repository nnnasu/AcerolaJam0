using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Abilities.Instances {
    [Serializable]
    public class ModifierInstance {
        public ModifierDefinition definition;
        public int level = 1;

        public ModifierInstance(ModifierDefinition def) {
            this.definition = def;

        }

        public void OnActivate(AbilityManager owner, AbilityInstance ability, Vector3 target, Action<AttributeSet> OnHit = null) {
            definition.OnActivate(owner, ability, target, this, OnHit);
        }
        public void OnHit(AbilityManager owner, AbilityInstance ability, AttributeSet hitTarget) {
            definition.OnHit(owner, ability, this, hitTarget);
        }

        public string ToTooltipText() {
            return definition.GetTooltipText(level);
        }
    }
}