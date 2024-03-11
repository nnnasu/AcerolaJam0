using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Abilities.Instances {
    [Serializable]
    public class ModifierInstance {
        public AbilityModifierDefinition definition;
        public int level = 1;

        public ModifierInstance(AbilityModifierDefinition def, int level = 1) {
            this.definition = def;
            this.level = level;
        }

        public void OnActivate(AbilityManager owner, AbilityInstance ability, Vector3 target, Action<AttributeSet> OnHit = null) {
            definition.OnActivate(owner, ability, target, this, OnHit);
        }
        public void OnHit(AbilityManager owner, AbilityInstance ability, AttributeSet hitTarget) {
            definition.OnHit(owner, ability, this, hitTarget);
        }
        public string GetTitle() {
            return "NA";
        }
        public string GetDescription() {
            if (definition == null) return "";
            return definition.GetTooltipText(level);
        }
    }
}