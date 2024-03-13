using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using Core.Abilities.Effects;
using Core.Abilities.Instances;
using Core.AbilityExtensions.Spawns;
using Core.AttributeSystem;
using Core.Utilities.Scaling;
using UnityEngine;


namespace Core.AbilityExtensions.Effects {
    [CreateAssetMenu(fileName = "ExtraProjectile", menuName = "Ability System/On Hit Effects/Projectile On Hit", order = 0)]
    public class SpawnProjectileOnHit : OnHitEffect {

        [Header("Projectile Settings")]
        public GameObject ProjectilePrefab;
        public float ProjectileSpeed = 10;
        public float ProjectileDuration = 5;
        public bool Piercing = false;
        public int ProjectileCount = 3;
        public float AngleBetweenProjectiles = 10;
        public bool ProjectileCountScalesWithLevel = false;
        public ScaledFloat DamageMultiplier;
        public EntityType IgnoredEntities = EntityType.Player;

        public EntityType TriggerEntities = EntityType.Structure;


        public override string GetTooltip(int level) {
            return "";
        }

        public override void OnHit(AbilityManager owner, AbilityInstance ability, ActionInstance action, IDamageable target) {
            if ((target.GetEntityType() & TriggerEntities) == 0) return;

            Vector3 pos = target.GetTransform().position;
            Vector3 direction = owner.transform.position - pos;
            direction.y = 0;
            direction.Normalize();


            int count = ProjectileCountScalesWithLevel ? action.level : ProjectileCount;

            // e.g. if we have 3 projectiles at 10 deg, leftmost starts at -15 deg so that the center is unchanged.
            float eulerY = -count * AngleBetweenProjectiles / 2;
            for (int i = 0; i < count; i++) {
                SpawnAndOrientProjectile(owner, ability, action, pos, Quaternion.Euler(0, eulerY, 0) * direction);
                eulerY += AngleBetweenProjectiles;
            }

        }

        private Projectile SpawnAndOrientProjectile(AbilityManager owner, AbilityInstance ability, ActionInstance action, Vector3 pos, Vector3 direction) {
            var obj = GlobalPool.Current.GetObject(ProjectilePrefab);
            if (obj == null) return null;
            var proj = obj.GetComponent<Projectile>();
            if (proj == null) return null;

            proj.transform.position = pos;


            float damage = Formulas.DamageDealtFormula(
                owner.Attributes.BaseAttack,
                DamageMultiplier.GetValueAtLevel(action.level),
                owner.Attributes.DamageDealtMult
            );

            proj.Activate(ProjectileDuration, ProjectileSpeed, direction, damage);
            proj.IgnoredEntities = IgnoredEntities;
            proj.DestroyOnContact = !Piercing;
            return proj;

        }

    }
}