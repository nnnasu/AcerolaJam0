using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using Core.Abilities.Definitions;
using Core.Abilities.Instances;
using Core.AbilityExtensions.Spawns;
using Core.AttributeSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile Action", menuName = "Player Actions/Fire Projectile", order = 0)]
public class ProjectileAction : ActionDefinition {
    public GameObject projectilePrefab;
    public float ProjectileSpeed = 10;
    public float ProjectileDuration = 5;
    public bool Piercing = false;
    public Vector3 offset = new Vector3(0, 1, 0);

    public EntityType IgnoredEntities = EntityType.Player;

    public override void ActivateAbility(AbilityManager owner, AbilityInstance ability, ActionInstance action, Vector3 target, Action<AttributeSet> OnHit = null) {
        Vector3 direction = target - owner.transform.position;
        direction.y = 0;
        direction.Normalize();
        var poolObj = GlobalPool.Current.GetObject(projectilePrefab);
        Projectile obj = poolObj.GetComponent<Projectile>();
        if (obj == null) return;

        obj.transform.position = owner.transform.position + owner.transform.rotation * offset;
        float damage = Formulas.DamageDealtFormula(
            owner.Attributes.BaseAttack,
            DamageMultiplier.GetValueAtLevel(action.level),
            owner.Attributes.DamageDealtMult
        );
        obj.Activate(ProjectileDuration, ProjectileSpeed, direction, damage, OnHit);
        obj.IgnoredEntities = IgnoredEntities;
        obj.DestroyOnContact = !Piercing;

        // if (useCurve) obj.Activate(curve, duration, direction, OnHit);
        // else obj.Activate(speed, duration, direction, OnHit);
    }

}
