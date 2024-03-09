using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using Core.Abilities.Definitions;
using Core.Abilities.Instances;
using Core.AbilityExtensions.Spawns;
using Core.AttributeSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "Line Action", menuName = "Ability System/Player Actions/Line Action", order = 0)]
public class LineDamageAction : ActionDefinition {
    public GameObject AoeSpawnObject;
    public EntityType IgnoredEntities = EntityType.Player;
    public float radius = 1;

    public override void ActivateAction(AbilityManager owner, AbilityInstance ability, ActionInstance action, Vector3 target, Action<AttributeSet> OnHit = null) {
        base.ActivateAction(owner, ability, action, target, OnHit);
        Vector3 direction = target - owner.transform.position;
        direction.y = 0;
        direction.Normalize();
        var poolObj = GlobalPool.Current.GetObject(AoeSpawnObject);
        LineAoe obj = poolObj.GetComponent<LineAoe>();
        if (obj == null) return;

        Vector3 finalPos = owner.transform.position + Range.GetValueAtLevel(action.level) * direction;

        obj.transform.position = finalPos;
        float damage = Formulas.DamageDealtFormula(
                    owner.Attributes.BaseAttack,
                    DamageMultiplier.GetValueAtLevel(action.level),
                    owner.Attributes.DamageDealtMult
                );
        obj.Activate(damage, OnHit);
        obj.ResizeAndMoveColliderToFitLength(owner.previousPosition, finalPos, 1);
        obj.IgnoredEntities = IgnoredEntities;


        // if (useCurve) obj.Activate(curve, duration, direction, OnHit);
        // else obj.Activate(speed, duration, direction, OnHit);
    }

}
