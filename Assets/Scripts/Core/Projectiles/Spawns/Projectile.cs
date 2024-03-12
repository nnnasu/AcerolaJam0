using System;
using System.Collections;
using System.Collections.Generic;
using Core.AttributeSystem;
using Core.Projectiles.Visuals;
using Core.Utilities.Sounds;
using PrimeTween;
using UnityEngine;
using UnityEngine.Pool;

namespace Core.AbilityExtensions.Spawns {
    public class Projectile : PoolableBehaviour {
        public float damage = 0;
        public bool DestroyOnContact = false;
        public float DamageDropoffRate = 0.9f; // every hit reduces damage by this amount.
        public float speed = 0;
        public Action<AttributeSet> OnHitCallback = null;
        protected Tween ExpiryTween;
        public EntityType IgnoredEntities = EntityType.NONE;
        public GameObject OnHitParticlesPrefab;
        public TrailRenderer trail;

        public AudioSource audioPlayer;
        public SoundGroup OnFireSounds;
        public SoundGroup OnHitSounds;

        public Transform HomingTarget = null;
        public float TurnRate = 0.5f;

        private void OnTriggerEnter(Collider other) {
            var target = other.GetComponent<IDamageable>();
            if (target == null) return;

            if ((IgnoredEntities & target.GetEntityType()) != 0) {
                // No overlap between flag objects.
                return;
            }

            target.TakeDamage(damage);
            damage *= DamageDropoffRate;
            
            if (target is AttributeSet attribute) {
                OnHitCallback?.Invoke(attribute);
            }

            if (OnHitSounds) audioPlayer.PlayOneShot(OnHitSounds.GetRandomClip());
            if (OnHitParticlesPrefab) SpawnHitParticles();

            if (DestroyOnContact) {
                ExpiryTween.Complete();
            }
        }

        public void Activate(float duration, float speed, Vector3 directionWS, float damage, Action<AttributeSet> onHitCallback = null) {
            enabled = true;
            if (trail) {
                trail.Clear();
                trail.emitting = true;
            }
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

        protected void SpawnHitParticles() {
            var obj = GlobalPool.Current.GetObject(OnHitParticlesPrefab);
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
        }

        protected void OnExpiry() {
            if (trail) trail.emitting = false;
            ReturnToPool();
        }

        private void Update() {
            if (HomingTarget) {
                Quaternion targetRotation = Quaternion.LookRotation(HomingTarget.position - transform.position);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, TurnRate);
            }
            transform.position += transform.forward * speed * Time.deltaTime;

        }





    }
}