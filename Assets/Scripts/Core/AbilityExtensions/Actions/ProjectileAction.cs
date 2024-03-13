using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using Core.Abilities.Definitions;
using Core.Abilities.Instances;
using Core.AbilityExtensions.Spawns;
using Core.AttributeSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile Action", menuName = "Ability System/Player Actions/Projectile", order = 0)]
public class ProjectileAction : ActionDefinition {
    public GameObject projectilePrefab;
    public float ProjectileSpeed = 10;
    public float ProjectileDuration = 5;
    public bool Piercing = false;
    public Vector3 offset = new Vector3(0, 1, 0);

    public int ProjectileCount = 3;
    public float AngleBetweenProjectiles = 10;

    public bool ProjectileCountScalesWithLevel = false;
    public bool HomeTowardsUser = false;
    public bool SpawnAtTargetPoint = false;


    protected override void ActivateActionImplementation(AbilityManager owner, AbilityInstance ability, ActionInstance action, Vector3 target, Action<IDamageable> OnHit = null) {
        Vector3 direction = target - owner.transform.position;
        direction.y = 0;
        direction.Normalize();

        Vector3 targetPoint = owner.transform.position + direction * Range.GetValueAtLevel(action.level);

        int count = ProjectileCountScalesWithLevel ? action.level : ProjectileCount;

        // e.g. if we have 3 projectiles at 10 deg, leftmost starts at -15 deg so that the center is unchanged.
        float eulerY = -count * AngleBetweenProjectiles / 2;
        for (int i = 0; i < count; i++) {
            SpawnAndOrientProjectile(owner, ability, action, targetPoint, Quaternion.Euler(0, eulerY, 0) * direction, OnHit);
            eulerY += AngleBetweenProjectiles;
        }

    }

    private Projectile SpawnAndOrientProjectile(AbilityManager owner, AbilityInstance ability, ActionInstance action, Vector3 target, Vector3 direction, Action<AttributeSet> OnHit = null) {
        var poolObj = GlobalPool.Current.GetObject(projectilePrefab);
        Projectile obj = poolObj.GetComponent<Projectile>();
        if (obj == null) {
            return null;
        }

        Vector3 position = Vector3.zero;
        if (SpawnAtTargetPoint) {
            position = target;
        } else position = owner.transform.position + owner.transform.rotation * offset;

        obj.transform.position = position;
        float damage = Formulas.DamageDealtFormula(
            owner.Attributes.BaseAttack,
            DamageMultiplier.GetValueAtLevel(action.level),
            owner.Attributes.DamageDealtMult
        );
        if (HomeTowardsUser) obj.HomingTarget = owner.transform;
        else obj.HomingTarget = null;

        obj.Activate(ProjectileDuration, ProjectileSpeed, direction, damage, OnHit);
        obj.IgnoredEntities = IgnoredEntities;
        obj.DestroyOnContact = !Piercing;
        return obj;
    }


}
