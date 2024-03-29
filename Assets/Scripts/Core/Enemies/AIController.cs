using System.Collections;
using System.Collections.Generic;
using Core.Enemies.Strategy;
using Core.GlobalInfo;
using PrimeTween;
using UnityEngine;

namespace Core.Enemies {
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(AttributeSet))]
    public class AIController : PoolableBehaviour {
        public Rigidbody rb;
        public AttributeSet attributes;
        public float TickRate = 0.5f;
        Tween TickTween;
        Tween RotationTween;
        public AIPackage Strategy;
        public float Turn180Duration = 1;
        public AIActionBase CurrentAction;
        public EnemyAnimationHandler enemyAnimationHandler;

        internal Vector3 currentPlayerPosition;
        internal float currentPlayerDistance;
        internal Vector3 currentPlayerDirection;



        private void OnEnable() {
            attributes.OnDeath += DeathCleanup;
            rb.velocity = Vector3.zero;
            TickTween.Stop();
            Tick();
        }
        private void OnDisable() {
            attributes.OnDeath -= DeathCleanup;
            TickTween.Stop();
        }

        /// <summary>
        /// Function that calls itself using tweens. 
        /// </summary>
        private void Tick() {
            Vector3? playerPos = PlayerLocation.CurrentLocator?.playerLocation;
            if (playerPos.HasValue) {
                currentPlayerPosition = playerPos.Value;
                Vector3 dir = currentPlayerPosition - transform.position;
                currentPlayerDistance = dir.magnitude;
                currentPlayerDirection = dir.normalized;
            }

            float delay = Strategy.ExecuteNextAction(this, playerPos);


            // TODO: Block rotations based on action??
            if (playerPos.HasValue) {
                rb.angularVelocity = Vector3.zero;
                // Rotate towards player
                // TODO: Lock rotations to y axis only
                Vector3 dir = playerPos.Value - transform.position;
                dir.y = 0;
                Quaternion start = transform.rotation.normalized;
                Quaternion target = Quaternion.LookRotation(dir).normalized;
                float duration = Mathf.Max(Quaternion.Angle(start, target) / 180 * Turn180Duration, float.Epsilon);
                // RotationTween.Stop();
                if (start != target) Tween.RigidbodyMoveRotation(rb, target, duration);
            }

            TickTween = Tween.Delay(Mathf.Max(TickRate, delay), Tick);
        }

        private void DeathCleanup(AttributeSet attributeSet) {
            rb.velocity = Vector3.zero;
            TickTween.Stop();

            if (enemyAnimationHandler) {
                float delay = enemyAnimationHandler.SetDeath();
                Tween.Delay(delay, ReturnToPool);
            } else {
                ReturnToPool();
            }
        }


        private void OnValidate() {
            if (rb == null) rb = GetComponent<Rigidbody>();
            if (attributes == null) attributes = GetComponent<AttributeSet>();
        }



    }
}