using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;

namespace Core.Projectiles.Visuals {
    public class PoolableParticleSystem : PoolableBehaviour {

        public ParticleSystem particles;
        public float ExpiryTime = 3;

        private void OnEnable() {
            particles.Play();
            Tween.Delay(ExpiryTime, Expire);
        }

        private void OnDisable() {
            particles.Stop();
        }


        private void Expire() {
            particles.Stop();
            ReturnToPool();
        }

    }
}