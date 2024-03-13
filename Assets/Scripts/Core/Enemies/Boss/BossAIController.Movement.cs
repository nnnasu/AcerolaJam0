using System.Collections;
using System.Collections.Generic;
using Core.GlobalInfo;
using UnityEngine;

namespace Core.Enemies.Boss {
    public partial class BossAIController : MonoBehaviour {


        private void MoveAndRotate(float time) {
            if (!PlayerLocation.CurrentLocator.playerLocation.HasValue) return;

            Vector3 diff = PlayerLocation.CurrentLocator.playerLocation.Value - transform.position;
            Vector3 dir = diff.normalized;

            if (CanMove) {
                Vector3 delta = CurrentMovementStrategy.GetWorldSpaceMovementVector(transform, cachedPlayerLocation, attributes.MovementSpeed);
                characterController.Move(delta * time);
                animationHandler.SetWalkAnimationDirection(delta);
            }
            // turning to face player
            float angle = Vector3.SignedAngle(transform.forward, dir, Vector3.up);
            if (CanTurn) transform.Rotate(new Vector3(0, angle, 0) * TurnRate * time);

            RegroundCharacter();
        }

        private void RegroundCharacter() {
            RaycastHit hit;
            Vector3 foot = characterController.bounds.center + ((characterController.height / 2) * Vector3.down);
            if (Physics.Raycast(foot, Vector3.down, out hit)) {
                Vector3 dist = hit.point - foot;
                characterController.Move(dist);
            }
        }

        public void Teleport(Vector3 target) {
            Vector3 delta = target - transform.position;
            characterController.Move(delta);
        }

    }
}