using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;

namespace Juice.Footsteps {
    public class FootStepsManager : MonoBehaviour {
        public AudioSource source;
        public FootstepSettings settings;


    // TO REMOVE LATER
        public float delay;
        Tween tween;
        public string currentTag;
        private void Start() {
            FootstepLoop();
        }

        private void FootstepLoop() {
            PlayFootsteps();
            tween.Stop();
            tween = Tween.Delay(delay, FootstepLoop);
        }

        public void PlayFootsteps() {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 2, settings.RaycastSettings, QueryTriggerInteraction.UseGlobal)) {
                currentTag = hit.collider.tag;
                if (settings.GetAudio(hit.collider.tag) is AudioClip clip) {
                    source.PlayOneShot(clip);
                }
            }

        }

    }
}