using Core.AbilityExtensions.Spawns;
using Core.AttributeSystem;
using Core.GlobalInfo;
using Core.Utilities.Scaling;
using PrimeTween;
using UnityEngine;

namespace Core.Enemies.Strategy {
    [CreateAssetMenu(fileName = "AttackCommand", menuName = "Enemy AI/Actions/Projectile", order = 11)]
    public class ProjectileAction : AIActionBase {

        public GameObject projectile;
        public float speed = 0;
        public float duration = 0.5f;
        public ScaledFloat damageMult;

        [Tooltip("Point at which the projectile is released in the attack duration.")]
        [Range(0, 1)]
        public float CastPoint = 0;
        public float BaseAttackTime = 2;
        public string AnimatorState;

        public Vector3 offset = new(0, 1, 1);
        public Vector3 Movement = Vector3.forward;

        public EntityType IgnoredEntities = EntityType.Enemy | EntityType.EnemyStructure;
        public bool Piercing = false;

        public override float Execute(AIController controller, AIPackage package, Vector3? playerPosition) {
            if (!playerPosition.HasValue) {
                controller.rb.velocity = Vector3.zero;
                return 0;
            }
            controller.rb.velocity = controller.rb.rotation * Movement;

            float attackTime = Formulas.AttackSpeedFormula(BaseAttackTime, controller.attributes.AttackSpeed);
            float damage = Formulas.DamageDealtFormula(
                        controller.attributes.BaseAttack,
                        damageMult.GetValueAtLevel(GameLevel.current.level),
                        controller.attributes.DamageDealtMult
                    );
            if (controller.GetComponent<EnemyAnimationHandler>() is EnemyAnimationHandler enemyAnimationHandler) {
                enemyAnimationHandler.SetMovement(false);
                float mult = BaseAttackTime / attackTime;
                enemyAnimationHandler.SetAttackTrigger(mult);
            }


            Tween.Delay(attackTime * CastPoint, () => SpawnProjectile(damage, controller));
            return attackTime;
        }
        private void SpawnProjectile(float damage, AIController controller) {
            var obj = GlobalPool.Current.GetObject(projectile);
            var proj = obj.GetComponent<Projectile>();
            if (!proj) return;

            proj.transform.position = controller.transform.position + controller.transform.rotation * offset;

            proj.Activate(duration, speed, controller.transform.forward, damage);
            proj.IgnoredEntities = IgnoredEntities;
            proj.DestroyOnContact = !Piercing;
        }



    }
}