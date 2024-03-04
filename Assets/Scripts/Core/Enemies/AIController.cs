using System.Collections;
using System.Collections.Generic;
using Core.Enemies.Strategy;
using Core.GlobalInfo;
using PrimeTween;
using UnityEngine;

namespace Core.Enemies {

    public class AIController : PoolableBehaviour {
        public Rigidbody rb;
        public AttributeSet attributes;
        public float TickRate = 0.5f;
        Tween TickTween;
        Tween RotationTween;
        public AIPackage Strategy;
        public float Turn180Duration = 1;

        private void Start() {
            Tick();
        }

        private void OnEnable() {
            attributes.OnDeath += (x) => DeathCleanup();
        }
        private void OnDisable() {

        }

        /// <summary>
        /// Function that calls itself using tweens. 
        /// </summary>
        private void Tick() {
            Vector3? playerPos = PlayerLocation.CurrentLocator?.playerLocation;
            float delay = Strategy.ExecuteNextAction(this, playerPos);
            // TODO: Block rotations based on action??
            if (playerPos.HasValue) {
                // Rotate towards player
                Vector3 dir = playerPos.Value - transform.position;
                dir.y = 0;
                Quaternion start = transform.rotation;
                Quaternion target = Quaternion.LookRotation(dir);
                float duration = Mathf.Max(Quaternion.Angle(start, target) / 180 * Turn180Duration, float.Epsilon);
                RotationTween.Stop();
                RotationTween = Tween.RigidbodyMoveRotation(rb, start, target, duration);
            }

            TickTween = Tween.Delay(Mathf.Max(TickRate, delay), Tick);
        }

        private void DeathCleanup() {
            // TODO return to pool
            TickTween.Stop();
            Destroy(gameObject);
        }




    }
}