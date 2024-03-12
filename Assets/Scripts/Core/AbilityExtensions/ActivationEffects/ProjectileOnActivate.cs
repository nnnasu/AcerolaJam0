
using System;
using System.Collections.Generic;
using Core.Abilities;
using Core.Abilities.Effects;
using Core.Abilities.Instances;
using Core.AbilityExtensions.Spawns;
using Core.AttributeSystem;
using Core.AttributeSystem.Conditions;
using Core.Utilities.Scaling;
using UnityEngine;

namespace Core.AbilityExtensions.ActivationEffects {
    [CreateAssetMenu(fileName = "ProjectileActivationEffect", menuName = "Ability System/On Activate Effects/Projectile", order = 0)]
    public class ProjectileOnActivate : OnActivateEffect {

        [Header("Ability Parameters")]
        public ScaledFloat DamageMultiplier;
        public EntityType IgnoredEntities = EntityType.Player;

        public List<OnActivateEffect> OnActivateEffects = new();
        public List<OnHitEffect> OnHitEffects = new();

        // This whole part is just copied from ProjectileAction
        public GameObject projectilePrefab;
        public float ProjectileSpeed = 10;
        public float ProjectileDuration = 5;
        public bool Piercing = false;
        public Vector3 offset = new Vector3(0, 1, 0);

        public int ProjectileCount = 1;
        public float AngleBetweenProjectiles = 10;
        [TextArea] public string Description;



        public void ActivateOnHitEffect(AbilityManager owner, AbilityInstance ability, ActionInstance action, AttributeSet target) {
            OnHitEffects.ForEach(x => x.OnHit(owner, ability, action, target));
        }


        public override string GetTooltip(int level) {
            return Description;
        }

        public override void OnActivateImpl(AbilityManager owner, AbilityInstance ability, ActionInstance action, Action<AttributeSet> OnHit = null) {
            Vector3 direction = owner.transform.forward;
            direction.y = 0;
            direction.Normalize();


            int count = ProjectileCount;

            // e.g. if we have 3 projectiles at 10 deg, leftmost starts at -15 deg so that the center is unchanged.
            float eulerY = -count * AngleBetweenProjectiles / 2;
            for (int i = 0; i < count; i++) {
                SpawnAndOrientProjecitle(owner, ability, action, owner.transform.position, Quaternion.Euler(0, eulerY, 0) * direction, OnHit);
                eulerY += AngleBetweenProjectiles;
            }
        }

        private Projectile SpawnAndOrientProjecitle(AbilityManager owner, AbilityInstance ability, ActionInstance action, Vector3 target, Vector3 direction, Action<AttributeSet> OnHit = null) {
            var poolObj = GlobalPool.Current.GetObject(projectilePrefab);
            Projectile obj = poolObj.GetComponent<Projectile>();
            if (obj == null) {
                return null;
            }

            Vector3 position = owner.transform.position + owner.transform.rotation * offset;

            obj.transform.position = position;
            float damage = Formulas.DamageDealtFormula(
                owner.Attributes.BaseAttack,
                DamageMultiplier.GetValueAtLevel(action.level),
                owner.Attributes.DamageDealtMult
            );


            Action<AttributeSet> combinedAction = delegate { };
            if (OnHit != null) combinedAction += OnHit;
            combinedAction += (target) => ActivateOnHitEffect(owner, ability, action, target);

            obj.HomingTarget = null;
            obj.Activate(ProjectileDuration, ProjectileSpeed, direction, damage, combinedAction);
            obj.IgnoredEntities = IgnoredEntities;
            obj.DestroyOnContact = !Piercing;
            return obj;
        }

    }
}