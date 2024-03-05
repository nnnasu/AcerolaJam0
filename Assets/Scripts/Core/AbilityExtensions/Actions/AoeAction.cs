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
public class AoeAction : ActionDefinition {
    public GameObject AoeSpawnObject;
    public EntityType IgnoredEntities = EntityType.Player;

    public override void ActivateAbility(AbilityManager owner, AbilityInstance ability, ActionInstance action, Vector3 target, Action<AttributeSet> OnHit = null) {
        Vector3 direction = target - owner.transform.position;
        direction.y = 0;
        direction.Normalize();
        var poolObj = GlobalPool.Current.GetObject(AoeSpawnObject);
        AreaBurst obj = poolObj.GetComponent<AreaBurst>();
        if (obj == null) return;

        obj.transform.position = owner.transform.position;
        float damage = DamageMultiplier.GetValueAtLevel(action.level) * owner.Attributes.BaseAttack;

        obj.Activate(damage, OnHit);
        obj.IgnoredEntities = IgnoredEntities;

        // if (useCurve) obj.Activate(curve, duration, direction, OnHit);
        // else obj.Activate(speed, duration, direction, OnHit);
    }

}