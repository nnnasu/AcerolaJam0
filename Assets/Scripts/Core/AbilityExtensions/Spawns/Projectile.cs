using System;
using System.Collections;
using System.Collections.Generic;
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

        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player") return; // TODO: filter player/enemies properly
            var attribute = other.GetComponent<AttributeSet>();
            if (!attribute) return;
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