using UnityEngine;

namespace Core.Enemies.Strategy {
    [CreateAssetMenu(fileName = "MoveCommand", menuName = "Enemy AI/Actions/Movement", order = 11)]
    public class MoveAction : AIActionBase{

        public float VelocityMultiplier = 1;

        public override float Execute(AIController controller, AIPackage package, Vector3? playerPosition) {
            if (!playerPosition.HasValue) {
                controller.rb.velocity = Vector3.zero;
                return 0;
            }

            Vector3 direction = playerPosition.Value - controller.transform.position;            
            direction.Normalize();
            controller.rb.velocity = VelocityMultiplier * controller.attributes.MovementSpeed * direction;

            if (controller.GetComponent<EnemyAnimationHandler>() is EnemyAnimationHandler enemyAnimationHandler) {
                enemyAnimationHandler.SetMovement(true, VelocityMultiplier);
            }
            return 0;
        }

    }
}