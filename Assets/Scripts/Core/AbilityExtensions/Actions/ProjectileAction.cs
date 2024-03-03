using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using Core.Abilities.Definitions;
using Core.Abilities.Instances;
using Core.AbilityExtensions.Spawns;
using UnityEngine;

public class ProjectileAction : ActionDefinition {
    public GameObject projectilePrefab;

    public override void ActivateAbility(AbilityManager owner, AbilityInstance ability, ActionInstance action, Vector3 target, Action<AttributeSet> OnHit = null) {
        Vector3 direction = target - owner.transform.position;
        direction.y = 0;
        direction.Normalize();
        var poolObj = GlobalPool.Current.GetObject(projectilePrefab);
        Projectile obj = poolObj.GetComponent<Projectile>();
        if (obj == null) return;

        obj.transform.position = owner.transform.position;

        // TODO: Implement Projectile

        // if (useCurve) obj.Activate(curve, duration, direction, OnHit);
        // else obj.Activate(speed, duration, direction, OnHit);
    }

    public override void OnHit(AbilityManager owner, AbilityInstance ability, ActionInstance action, AttributeSet target) {
    }
}
