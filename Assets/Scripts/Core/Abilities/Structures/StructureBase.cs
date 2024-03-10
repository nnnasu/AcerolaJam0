using System.Collections;
using System.Collections.Generic;
using Core.AttributeSystem;
using PrimeTween;
using UnityEngine;
using UnityEngine.Pool;

namespace Core.Abilities.Structures {
    public class StructureBase : PoolableBehaviour {
        public StructureAttributes attributes;
        public EntityType IgnoredEntities = EntityType.NONE;
        public float TickInterval = 2;
        public float DamagePerTick;
        Tween TickTween;
        AbilityManager owner;

        public void Activate(float tickInterval, AbilityManager owner) {
            TickInterval = tickInterval;
            attributes.ResetState();
            this.owner = owner;
            TickTween = Tween.Delay(TickInterval, OnTick);
            attributes.OnDeath += OnDeath;
        }


        public void OnTick() {
            TickTween.Stop();
            attributes.TakeDamage(DamagePerTick);
            TickAction();
            TickTween = Tween.Delay(TickInterval, OnTick);
        }

        protected virtual void ActivateAction() {

        }
        protected virtual void TickAction() {
        }

        protected virtual void RecallAction() {

        }

        public void OnDeath(StructureAttributes attributes) {
            attributes.OnDeath -= OnDeath;
            TickTween.Stop();
            ReturnToPool();
        }

        public void OnRecall(AbilityManager manager) {
            RecallAction();
            attributes.OnDeath -= OnDeath;
            TickTween.Stop();
            ReturnToPool();
        }

    }
}