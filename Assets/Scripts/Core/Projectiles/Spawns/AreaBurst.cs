using System;
using Core.AttributeSystem;
using PrimeTween;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.VFX;

namespace Core.AbilityExtensions.Spawns {
    /// <summary>
    /// Class that activates some VFX/Particles and then does damage around itself.
    /// </summary>
    public class AreaBurst : PoolableBehaviour {

        public float damage = 0;
        public float delay;
        public float linger;
        public float returnTime;


        public Action<AttributeSet> OnHitCallback = null;
        Tween ExpandTween;
        Tween DelayTween;
        Tween ExpiryTween;
        public VisualEffect vfx;

        public EntityType IgnoredEntities = EntityType.NONE;

        public Collider col;

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
        }

        public void Activate(float damage, Action<AttributeSet> onHitCallback = null) {
            enabled = true;
            gameObject.SetActive(true);
            col.enabled = false;
            this.damage = damage;
            this.OnHitCallback = onHitCallback;

            if (delay > 0) DelayTween = Tween.Delay(delay, Burst);
            else Burst();
            
            vfx?.Play();
        }

        private void Burst() {
            col.enabled = true;
            if (linger > 0) ExpiryTween = Tween.Delay(linger, Deactivate);
            else Deactivate();
        }

        private void Deactivate() {
            col.enabled = false;
            if (returnTime > 0) ExpiryTween = Tween.Delay(returnTime, Expire);
            else Expire();
        }

        private void Expire() {
            ReturnToPool();
        }

    }
}