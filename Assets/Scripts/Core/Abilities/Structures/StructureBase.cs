using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities.Definitions;
using Core.Abilities.Instances;
using Core.AttributeSystem;
using Core.AttributeSystem.Alignments;
using PrimeTween;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

namespace Core.Abilities.Structures {
    public class StructureBase : PoolableBehaviour {
        public StructureAttributes attributes;
        public Vector3 Center;
        public float Radius;

        [Header("Structure Parameters")]
        public StructureDefinition Definition;
        public EntityType IgnoredEntities = EntityType.NONE;
        public int level = 1;
        public float TickInterval = 2;

        Tween TickTween;
        AbilityManager owner;

        // UnityEvents, should be used for visuals/sounds only (StructureEffect handles gameplay logic)
        public UnityEvent OnActivationEvent;
        public UnityEvent OnTickEvent;
        public UnityEvent OnRecallEvent;
        public UnityEvent OnDeathEvent;

        protected Collider[] CastResults = new Collider[100];

        public Collider[] OverlapAoe(out int count) {
            int mask = Definition.effects.LayersToCastFor;
            count = Physics.OverlapSphereNonAlloc( transform.position + Center, Radius, CastResults, mask, QueryTriggerInteraction.Ignore); // ignore?
            return CastResults;
        }


        public void Activate(float tickInterval, AbilityManager owner, StructureStorageInstance storage, float HPMultiplier = 1) {
            level = Mathf.FloorToInt(storage.AverageLevel);
            Radius = Definition.effects.Radius.GetValueAtLevel(level);
            TickInterval = tickInterval;
            attributes.ResetState(HPMultiplier);
            this.owner = owner;
            TickTween = Tween.Delay(TickInterval, OnTick);
            attributes.OnDeath += OnDeath;
        }


        public void OnTick() {
            TickTween.Stop();
            attributes.TakeDamage(1); // structures only ever take damage in instances of 1.
            TickAction();
            TickTween = Tween.Delay(TickInterval, OnTick);
        }

        protected virtual void ActivateAction() {
            OnActivationEvent?.Invoke();
        }

        protected virtual void TickAction() {
            OnTickEvent?.Invoke();
            Definition.effects.OnTick(this);
        }

        protected virtual void RecallAction(AbilityManager recaller) {
            OnRecallEvent?.Invoke();
            Definition.effects.OnRecall(recaller, this);
        }

        public void OnDeath(StructureAttributes attributes) {
            OnDeathEvent?.Invoke();
            Definition?.OnDeath(owner, this);

            Cleanup();
        }

        public void OnRecall(AbilityManager manager, bool executeAction = true) {
            if (executeAction) {
                RecallAction(manager);
            }
            Cleanup();
        }

        public void Cleanup() {
            attributes.OnDeath -= OnDeath;
            TickTween.Stop();
            ReturnToPool();
        }

        public float GetPercentageHP() {
            if (!attributes) return 0;
            if (attributes.MaxHP <= float.Epsilon) return 0;

            return attributes.HP / attributes.MaxHP;
        }


    }
}