using System;
using Core.AttributeSystem;
using PrimeTween;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering.Universal;
using UnityEngine.VFX;

namespace Core.AbilityExtensions.Spawns {
    /// <summary>
    /// Class that activates some VFX/Particles and then does damage around itself.
    /// </summary>
    public class LineAoe : PoolableBehaviour {

        public float damage = 0;
        public float delay = 0.05f;
        public float linger = 1.5f;


        public Action<AttributeSet> OnHitCallback = null;
        Tween DelayTween;
        Tween ExpiryTween;
        public VisualEffect vfx;
        public DecalProjector decalProjector;

        public EntityType IgnoredEntities = EntityType.NONE;

        public CapsuleCollider col;

        private void OnTriggerEnter(Collider other) {
            var target = other.GetComponent<AttributeSet>();
            if (!target) return;

            if ((IgnoredEntities & target.entityType) != 0) {
                // No overlap between flag objects.
                return;
            }
            Debug.Log(other.name);
            target.TakeDamage(damage);
            OnHitCallback?.Invoke(target);
        }

        public void ResizeAndMoveColliderToFitLength(Vector3 start, Vector3 end, float? radius = null) {
            col.height = Vector3.Distance(start, end);
            if (radius.HasValue) col.radius = radius.Value;
            Vector3 pos = start + (end - start) / 2;
            Quaternion rotation = Quaternion.LookRotation(end - start);
            transform.position = pos;
            transform.rotation = rotation;

            decalProjector.size = new Vector3(col.radius, 3, col.height);
            decalProjector.pivot = Vector3.zero;
        }

        public void Activate(float damage, Action<AttributeSet> onHitCallback = null) {
            enabled = true;
            gameObject.SetActive(true);
            col.enabled = false;
            decalProjector.enabled = false;
            this.damage = damage;
            this.OnHitCallback = onHitCallback;
            DelayTween = Tween.Delay(delay, Burst);
            vfx.Play();
        }

        private void Burst() {
            col.enabled = true;
            decalProjector.enabled = true;
            ExpiryTween = Tween.Delay(linger, Expire);
        }

        private void Expire() {
            ReturnToPool();
        }

    }
}