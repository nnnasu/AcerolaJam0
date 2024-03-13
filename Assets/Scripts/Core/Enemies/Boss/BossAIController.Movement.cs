using System.Collections;
using System.Collections.Generic;
using Core.GlobalInfo;
using UnityEngine;

namespace Core.Enemies.Boss {
    public partial class BossAIController : MonoBehaviour {


        private void MaintainPlayerDistanceAndOrientation(float time) {
            if (!PlayerLocation.CurrentLocator.playerLocation.HasValue) return;

            Vector3 diff = PlayerLocation.CurrentLocator.playerLocation.Value - transform.position;
            Vector3 dir = diff.normalized;
            float angle = Vector3.SignedAngle(transform.forward, dir, Vector3.up);
            float distance = diff.magnitude;
            float speed = Mathf.Lerp(0, attributes.MovementSpeed, Mathf.Clamp01(distance / distanceSoftRange));
            if (distance < desiredDistance) speed *= -1;

            if (Mathf.Abs(distance - desiredDistance) < deadzone) speed = 0;


            characterController.Move(speed * time * dir);
            transform.Rotate(new Vector3(0, angle, 0) * TurnRate * time);

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