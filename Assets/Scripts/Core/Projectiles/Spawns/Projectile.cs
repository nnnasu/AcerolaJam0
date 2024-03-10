using System;
using System.Collections;
using System.Collections.Generic;
using Core.AttributeSystem;
using Core.Utilities.Sounds;
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

        public AudioSource audioPlayer;
        public SoundGroup OnFireSounds;
        public SoundGroup OnHitSounds;

        private void OnTriggerEnter(Collider other) {
            var target = other.GetComponent<IDamageable>();
            if (target == null) return;

            if ((IgnoredEntities & target.GetEntityType()) != 0) {
                // No overlap between flag objects.
                return;
            }

            target.TakeDamage(damage);
            if (target is AttributeSet attribute) {
                OnHitCallback?.Invoke(attribute);
            }

            if (OnHitSounds) audioPlayer.PlayOneShot(OnHitSounds.GetRandomClip());

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
            if (OnFireSounds) {
                audioPlayer.PlayOneShot(OnFireSounds.GetRandomClip());
            }
        }



        private void OnExpiry() {
            ReturnToPool();
        }

        private void Update() {
            transform.position += transform.forward * speed * Time.deltaTime;
        }





    }
}