using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using Core.Abilities.Definitions;
using Core.Abilities.Instances;
using UnityEngine;

namespace Core.AbilityExtensions.Actions {
    [CreateAssetMenu(fileName = "TeleportAction", menuName = "Actions/Teleport Action", order = 0)]
    public class TeleportAction : ActionDefinition {


        public override void ActivateAbility(AbilityManager owner, AbilityInstance ability, ActionInstance action, Vector3 target, Action<AttributeSet> OnHit = null) {
            Vector3 direction = target - owner.transform.position;
            direction.y = 0;
            float mag = Mathf.Clamp(direction.magnitude, 0, Range.GetValueAtLevel(action.level));
            direction = direction.normalized * mag;
            Vector3 targetPoint = owner.transform.position + direction;

            var charcon = owner.GetComponent<CharacterController>();
            charcon.enabled = false;
            owner.transform.position = targetPoint;
            charcon.enabled = true;
        }

    }
}