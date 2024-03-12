using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using Core.Abilities.Definitions;
using Core.Abilities.Instances;
using Core.AbilityExtensions.Spawns;
using Core.AttributeSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "Teleport Trail", menuName = "Ability System/Player Actions/Teleport Trail", order = 0)]
public class TeleportTrailDamageAction : ActionDefinition {
    public GameObject AoeSpawnObject;
    public float radius = 1;

    protected override void ActivateActionImplementation(AbilityManager owner, AbilityInstance ability, ActionInstance action, Vector3 target, Action<AttributeSet> OnHit = null) {
        Vector3 direction = target - owner.previousPosition;
        var poolObj = GlobalPool.Current.GetObject(AoeSpawnObject);
        LineAoe obj = poolObj.GetComponent<LineAoe>();
        if (obj == null) return;

        float mag = Mathf.Clamp(direction.magnitude, 0, Range.GetValueAtLevel(action.level));
        direction.y = 0;
        direction = direction.normalized;

        Vector3 teleportTargetPoint = owner.transform.position + direction * mag;
        Vector3 lineEndPos = owner.transform.position + Range.GetValueAtLevel(action.level) * direction; // line should extend to max

        float damage = Formulas.DamageDealtFormula(
                    owner.Attributes.BaseAttack,
                    DamageMultiplier.GetValueAtLevel(action.level),
                    owner.Attributes.DamageDealtMult
                );
        obj.Activate(damage, OnHit);
        obj.ResizeAndMoveColliderToFitLength(owner.previousPosition, lineEndPos, 1);
        obj.IgnoredEntities = IgnoredEntities;
        owner.Teleport(teleportTargetPoint);
    }

}
