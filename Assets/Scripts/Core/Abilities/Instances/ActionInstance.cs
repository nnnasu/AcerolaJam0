using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities.Definitions;
using UnityEngine;

namespace Core.Abilities.Instances {

    [Serializable]
    public class ActionInstance {
        public ActionDefinition definition;
        public int level = 1;
        public float BaseCooldown => definition.BaseCooldown.GetValueAtLevel(level);
        public float CooldownDisplay;
        public float BaseCost => definition.BaseMPCost.GetValueAtLevel(level);
        public float DamageMultiplier => definition.DamageMultiplier.GetValueAtLevel(level);

        public ActionInstance(ActionDefinition definition, int level = 1) {
            this.definition = definition;
            this.level = level;

            CooldownDisplay = BaseCooldown;
        }

        public void ActivateAbility(AbilityManager owner, AbilityInstance ability, Vector3 target, Action<AttributeSet> OnHit = null) {
            definition.ActivateAction(owner, ability, this, target, OnHit, (target) => OnActionHit(owner, ability, target));
        }

        public void OnHit(AbilityManager owner, AbilityInstance ability, AttributeSet target) {
            definition.OnHit(owner, ability, this, target);
        }

        protected void OnActionHit(AbilityManager owner, AbilityInstance ability, AttributeSet target) {
            definition.OnActionHit(owner, ability, this, target);
        }

        public string GetTitle() {
            return "NA";
        }
        public string GetDescription() {
            if (definition == null) return "Empty Action";
            return definition.GetTooltipText(level);
        }

    }
}