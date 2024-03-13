using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using Core.Abilities.Definitions;
using Core.Abilities.Instances;
using Core.AbilityExtensions.Spawns;
using UnityEngine;

namespace Core.AbilityExtensions.Actions {
    [CreateAssetMenu(fileName = "Buff Action", menuName = "Ability System/Player Actions/Self-Buff", order = 0)]
    public class BuffAction : ActionDefinition {
        public GameObject followObjectPrefab;
        public StatusEffect BuffEffect;

        protected override void ActivateActionImplementation(AbilityManager owner, AbilityInstance ability, ActionInstance action, Vector3 target, Action<IDamageable> OnHit = null) {
            owner.Attributes.ApplyEffect(BuffEffect.GetEffectInstance(owner.Attributes, action.level));

            var obj = GlobalPool.Current.GetObject(followObjectPrefab);
            var buffVisual = obj.GetComponent<TransformFollowEffect>();

            float time = BuffEffect.duration.GetValueAtLevel(action.level);
            if (buffVisual) {
                buffVisual.StartFollow(owner.transform, time);
            }

            owner.Attributes.ApplyEffect(BuffEffect.GetEffectInstance(owner.Attributes, action.level));
        }

        public override string GetActionDescription(int level) {

            return base.GetActionDescription(level) + "\n" + BuffEffect.GetDescription(BuffEffect.GetEffectInstance(null, level));
        }
    }
}