using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using Core.Abilities.Definitions;
using Core.Abilities.Instances;
using Core.AbilityExtensions.Spawns;
using Core.AttributeSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "AOE Action", menuName = "Ability System/Player Actions/Point Targeted Spawn", order = 0)]
public class PointOriginSpawnAction : ActionDefinition {
    public GameObject AoeSpawnObject;

    protected override void ActivateActionImplementation(AbilityManager owner, AbilityInstance ability, ActionInstance action, Vector3 target, Action<IDamageable> OnHit = null) {
        Vector3 direction = target - owner.transform.position;
        direction.y = 0;
        direction.Normalize();
        var poolObj = GlobalPool.Current.GetObject(AoeSpawnObject);
        AreaBurst obj = poolObj.GetComponent<AreaBurst>();
        if (obj == null) return;

        Vector3 finalPos = owner.transform.position + Range.GetValueAtLevel(action.level) * direction;

        obj.transform.position = finalPos;
        float damage = Formulas.DamageDealtFormula(
            owner.Attributes.BaseAttack,
            DamageMultiplier.GetValueAtLevel(action.level),
            owner.Attributes.DamageDealtMult
        );

        obj.Activate(damage, OnHit);
        obj.IgnoredEntities = IgnoredEntities;

        // if (useCurve) obj.Activate(curve, duration, direction, OnHit);
        // else obj.Activate(speed, duration, direction, OnHit);
    }

}
