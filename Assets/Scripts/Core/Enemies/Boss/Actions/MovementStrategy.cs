using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Enemies.Boss.Actions {
    [CreateAssetMenu(fileName = "MovementStrategy", menuName = "MovementStrategy", order = 0)]
    public class MovementStrategy : ScriptableObject {

        [Tooltip("Facing the player, which direction should we travel in? Note that this will be normalized at runtime.")]
        public Vector3 movementDirectionRelative = Vector3.forward;

        public bool NegateSpeedIfCloserThanDesired = true;

        [Tooltip("")]
        public float DesiredDistance = 5;

        [Tooltip("When distance is within this distance from the desired distance, do not move.")]
        public float Deadzone = 3;

        [Tooltip("Lerp speed according to this distance. When at DesiredDistance+Deadzone+Softzone, speed is maximum and slows down as desired distance is approached.")]
        public float Softzone = 10;

        public Vector3 GetWorldSpaceMovementVector(Transform boss, Vector3 playerPos, float moveSpeed) {
            Vector3 diff = playerPos - boss.position;
            Vector3 direction = Quaternion.LookRotation(diff.normalized) * movementDirectionRelative.normalized;
            float distance = diff.magnitude;
            float speed = 1;
            if (distance < DesiredDistance + Deadzone && distance > DesiredDistance - Deadzone) speed = float.Epsilon;
            else {
                speed = Mathf.Lerp(0, moveSpeed, Mathf.Clamp01(Mathf.Abs(distance - DesiredDistance) / (Softzone + Deadzone + 0.01f)));
            }

            if (NegateSpeedIfCloserThanDesired && distance < DesiredDistance) speed *= -1;


            return direction * speed;
        }

    }
}