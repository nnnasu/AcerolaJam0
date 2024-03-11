using System.Collections;
using System.Collections.Generic;
using Core.AbilityExtensions.Spawns;
using UnityEngine;

namespace Core.AbilityExtensions.Spawns {
    public class ForceProjectile : Projectile {

        public float ForceMagnitude = 5;

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
            if (OnHitParticlesPrefab) SpawnHitParticles();

            if (DestroyOnContact) {
                ExpiryTween.Complete();
            }
        }

        void OnTriggerStay(Collider other) {
            if (other.GetComponent<Rigidbody>() is Rigidbody rb) {
                if (rb.isKinematic) return;
                Vector3 dir = transform.position - rb.transform.position;
                rb.AddForce(dir.normalized * ForceMagnitude, ForceMode.Force);
            }

        }
    }
}