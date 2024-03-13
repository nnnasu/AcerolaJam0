using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Animation {
    public class AnimationHandler : MonoBehaviour {
        public static int MoveXHash = Animator.StringToHash("MoveX");
        public static int MoveYHash = Animator.StringToHash("MoveY");
        public static int AnimationSpeedHash = Animator.StringToHash("ActionSpeedMult");
        public static int EmptyStateHash = Animator.StringToHash("Empty State");
        public bool UseEmptyState = true;

        public Animator animator;

        public void SetWalkAnimationDirection(Vector3 directionWS) {
            var relativeRot = Quaternion.FromToRotation(transform.forward, Vector3.forward);
            Vector3 directionOS = (relativeRot * directionWS).normalized;
            animator.SetFloat(MoveXHash, directionOS.x);
            animator.SetFloat(MoveYHash, directionOS.z);
        }

        public void PlayAnimationState(AnimationStateInfo state) {
            if (!state) return;
            if (UseEmptyState) animator.Play(EmptyStateHash);
            animator.Play(state.StateHash);
        }
        

        public void SetActionSpeed(float speed) {
            animator.SetFloat(AnimationSpeedHash, speed);
        }






    }
}
