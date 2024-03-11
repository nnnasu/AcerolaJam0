using System.Collections;
using System.Collections.Generic;
using Core.AbilityExtensions.Spawns;
using Core.AttributeSystem;
using Core.Enemies.Components;
using Core.GlobalInfo;
using Core.Utilities.Scaling;
using PrimeTween;
using UnityEngine;


namespace Core.Enemies.Strategy {
    [CreateAssetMenu(fileName = "MoveCommand", menuName = "Enemy AI/Actions/Explosion", order = 11)]
    public class ExplosionAction : AIActionBase {

        public float ChargeTimer;
        public float ChargeExplosionDelay;
        public float offset;
        public ScaledFloat damageMult;
        public GameObject ExplosionPrefab;
        public EntityType IgnoredEntities = EntityType.NONE;
        public float radius = 5;


        public override float Execute(AIController controller, AIPackage package, Vector3? playerPosition) {
            controller.rb.velocity = Vector3.zero;
            var ex = controller.GetComponent<ExplosionComponent>();
            if (!ex) return 0;

            switch (ex.ChargeStatus) {
                case ChargeStatus.Neutral: return ExecuteNeutral(controller, package, playerPosition, ex);
                case ChargeStatus.Charging: return ExecuteCharging(controller, package, playerPosition, ex);
                case ChargeStatus.Charged: return ExecuteCharged(controller, package, playerPosition, ex);

                default: return 0;
            }


        }

        private float ExecuteNeutral(AIController controller, AIPackage package, Vector3? playerPosition, ExplosionComponent ex) {
            ex.ChargeTimer = ChargeTimer;
            ex.ExplosionDelay = ChargeExplosionDelay;
            ex.StartCharge();

            return ChargeTimer + offset;
        }

        private float ExecuteCharging(AIController controller, AIPackage package, Vector3? playerPosition, ExplosionComponent ex) {
            return 0;
        }

        private float ExecuteCharged(AIController controller, AIPackage package, Vector3? playerPosition, ExplosionComponent ex) {
            if (controller.currentPlayerDistance > radius)  {
                // Cancel
                Debug.Log("Charged, should cancel");
                ex.CancelCharge();
                return 0;
            }
            // Detonate.
            ex.MarkForDetonation();
            // Spawn the burst

            var obj = GlobalPool.Current.GetObject(ExplosionPrefab);
            var burst = obj.GetComponent<AreaBurst>();
            if (!burst) return 0;

            float damage = Formulas.DamageDealtFormula(
                                    controller.attributes.MaxHP,
                                    damageMult.GetValueAtLevel(GameLevel.current.level),
                                    controller.attributes.DamageDealtMult);
                                    // let the explosion kill itself. The tick tween doesnt stop if we directly kill it here.

            burst.damage = damage;
            burst.IgnoredEntities = IgnoredEntities;
            burst.transform.position = controller.transform.position;
            burst.Activate(damage);

            // controller.attributes.TakeDamage(controller.attributes.MaxHP);

            return 5;
        }

    }
}