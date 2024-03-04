using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities.Enums;
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
            
            
            var attribute = other.GetComponent<AttributeSet>();
            if (!attribute) return;

            if ((IgnoredEntities & attribute.entityType) != 0) {
                // No overlap between flag objects.
                return;
            }
            
            attribute.TakeDamage(damage);
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
        }



        private void OnExpiry() {
            ReturnToPool();
        }

        private void Update() {
            transform.position += transform.forward * speed * Time.deltaTime;
        }





    }
}