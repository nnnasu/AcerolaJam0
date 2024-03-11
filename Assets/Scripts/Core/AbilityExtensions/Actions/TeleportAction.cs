using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using Core.Abilities.Definitions;
using Core.Abilities.Instances;
using UnityEngine;

namespace Core.AbilityExtensions.Actions {
    [CreateAssetMenu(fileName = "TeleportAction", menuName = "Ability System/Player Actions/Teleport Action", order = 0)]
    public class TeleportAction : ActionDefinition {


        protected override void ActivateActionImplementation(AbilityManager owner, AbilityInstance ability, ActionInstance action, Vector3 target, Action<AttributeSet> OnHit = null) {
            base.ActivateAction(owner, ability, action, target, OnHit);
            Vector3 direction = target - owner.transform.position;
            direction.y = 0;
            float mag = Mathf.Clamp(direction.magnitude, 0, Range.GetValueAtLevel(action.level));
            direction = direction.normalized * mag;
            Vector3 targetPoint = owner.transform.position + direction;
            owner.Teleport(targetPoint);
        }

    }
}