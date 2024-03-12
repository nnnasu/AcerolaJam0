using UnityEngine;


namespace Core.Animation {
    public class ActivatableAttachment : PoolableBehaviour {

        public ParticleSystem particles;
        public TrailRenderer trails;

        public virtual void Activate() {
            if (particles) {
                particles.Play();
            }
            
            if (trails) {
                trails.emitting = true;
            }

        }

        public virtual void Deactivate() {
            
            if (particles) {
                particles.Stop();
            }
            
            if (trails) {
                trails.Clear();
                trails.emitting = false;
            }

        }

    }
}