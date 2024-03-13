using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities.Definitions;
using Core.AttributeSystem;
using PrimeTween;
using UnityEngine;
using UnityEngine.Pool;

namespace Core.Abilities.Structures {
    public class StructureBase : PoolableBehaviour {
        public StructureAttributes attributes;
        public StructureDefinition Definition;
        public EntityType IgnoredEntities = EntityType.NONE;
        public float TickInterval = 2;
        public float DamagePerTick;
        Tween TickTween;
        AbilityManager owner;

        public void Activate(float tickInterval, AbilityManager owner, float HPMultiplier = 1) {
            TickInterval = tickInterval;
            attributes.ResetState(HPMultiplier);
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
            // Do damage to player
            Cleanup();
        }

        public void OnRecall(AbilityManager manager, bool executeAction = true) {
            if (executeAction) RecallAction();
            Cleanup();
        }

        public void Cleanup() {
            attributes.OnDeath -= OnDeath;
            TickTween.Stop();
            ReturnToPool();
        }



    }
}