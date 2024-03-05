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


        public Action<AttributeSet> OnHitCallback = null;
        Tween DelayTween;
        Tween ExpiryTween;
        public VisualEffect vfx;

        public EntityType IgnoredEntities = EntityType.NONE;

        public Collider col;

        private void OnTriggerEnter(Collider other) {            
            var target = other.GetComponent<AttributeSet>();
            if (!target) return;

            if ((IgnoredEntities & target.entityType) != 0) {
                // No overlap between flag objects.
                return;
            }
            target.TakeDamage(damage);
            OnHitCallback?.Invoke(target);            
        }

        public void Activate(float damage, Action<AttributeSet> onHitCallback = null) {
            enabled = true;
            gameObject.SetActive(true);
            col.enabled = false;
            this.damage = damage;
            this.OnHitCallback = onHitCallback;
            DelayTween = Tween.Delay(delay, Burst);
            vfx.Play();
        }

        private void Burst() {
            col.enabled = true;
            ExpiryTween = Tween.Delay(linger, Expire);
        }

        private void Expire() {
            ReturnToPool();

        }

    }
}