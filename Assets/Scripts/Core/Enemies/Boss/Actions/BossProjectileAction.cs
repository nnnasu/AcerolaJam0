using System.Collections;
using System.Collections.Generic;
using Core.AbilityExtensions.Spawns;
using Core.Animation;
using Core.Enemies.Boss;
using Core.Enemies.Boss.Actions;
using Core.GlobalInfo;
using Core.Utilities.Scaling;
using UnityEngine;
namespace Core.Enemies.Boss.Actions {
    [CreateAssetMenu(fileName = "Projectile", menuName = "Boss/Projectile", order = 0)]
    public class BossProjectileAction : BossAction {

        public GameObject projectile;
        public float ProjectileSpeed = 15;
        public float duration = 1f;
        public bool DestroyOnContact = false;
        public ScaledFloat damageMult;




        public override bool CanExecuteImpl(BossAIController boss) {
            return true;
        }

        public override void ExecuteImpl(BossAIController boss) {
            var obj = GlobalPool.Current.GetObject(projectile);
            var proj = obj.GetComponent<Projectile>();

            float damage = Formulas.DamageDealtFormula(boss.attributes.BaseAttack,
                damageMult.GetValueAtLevel(GameLevel.current.level),
                boss.attributes.DamageDealtMult);
            proj.Activate(duration, ProjectileSpeed, boss.transform.forward, damage);
            proj.IgnoredEntities = AttributeSystem.EntityType.Enemy;
            proj.DestroyOnContact = DestroyOnContact;
            proj.transform.position = boss.transform.position + Vector3.forward;
        }
    }
}