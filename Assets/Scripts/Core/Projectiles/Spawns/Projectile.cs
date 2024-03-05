using System;
using System.Collections;
using System.Collections.Generic;
using Core.AttributeSystem;
using PrimeTween;
using UnityEngine;
using UnityEngine.Pool;

namespace Core.AbilityExtensions.Spawns {
    public class Projectile : PoolableBehaviour {
        public float damage = 0;
        public bool DestroyOnContact = false;
        public float speed = 0;
        public Action<AttributeSet> OnHitCallback = null;
        Tween ExpiryTween;
        public EntityType IgnoredEntities = EntityType.NONE;

        private void OnTriggerEnter(Collider other) {
            var target = other.GetComponent<AttributeSet>();
            if (!target) return;

            if ((IgnoredEntities & target.entityType) != 0) {
                // No overlap between flag objects.
                return;
            }

            target.TakeDamage(damage);
            OnHitCallback?.Invoke(target);
            if (DestroyOnContact) {
                ExpiryTween.Complete();
            }
        }

        public void Activate(float duration, float speed, Vector3 directionWS, float damage, Action<AttributeSet> onHitCallback = null) {
            enabled = true;
            transform.rotation = Quaternion.LookRotation(directionWS);
            gameObject.SetActive(true);
            ExpiryTween = Tween.Delay(duration, OnExpiry);
            this.speed = speed;
            this.damage = damage;
            this.OnHitCallback = onHitCallback;
        }



        private void OnExpiry() {
            ReturnToPool();
        }

        private void Update() {
            transform.position += transform.forward * speed * Time.deltaTime;
        }





    }
}