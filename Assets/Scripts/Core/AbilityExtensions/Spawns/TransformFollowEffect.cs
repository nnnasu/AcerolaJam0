using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;


namespace Core.AbilityExtensions.Spawns {
    /// <summary>
    /// This just follows a target around without any specific behaviour. Mostly to be used for effects.
    /// </summary>
    public class TransformFollowEffect : PoolableBehaviour {

        public Transform Target;
        public Vector3 offset = Vector3.up;
        private bool Activated = false;

        public void StartFollow(Transform target, float time) {
            gameObject.SetActive(true);
            Activated = true;
            Target = target;
            Tween.Delay(time, Return);
        }

        void Update() {
            if (!Activated) return;
            transform.position = Target.position + offset;
            transform.rotation = Target.rotation;
        }

        private void Return() {
            gameObject.SetActive(false);
            ReturnToPool();
        }
    }
}