using System;
using System.Collections;
using System.Collections.Generic;
using Core.AbilitySystem.Abilities;
using UnityEngine;

[Serializable]
public class AbilityInstance {
    public AbilityTemplate BaseAbility;
    public AbilityManager owner;
    public int AbilityLevel = 1;

    public void ActivateAbility(Vector3 targetPosition) {
        int index = Math.Min(AbilityLevel - 1, BaseAbility.DamageScaling.Length);
        float damage = BaseAbility.DamageScaling[index]; // TODO Multiply by base attack value

        BaseAbility.Actions.ForEach(x => x.Execute(owner, targetPosition, damage, null, OnHit));
        // Modifiers.ForEach(x => x.OnActivate(...));
    }

    /// <summary>
    /// Deal with any other effects that should be called when an enemy is hit.
    /// </summary>
    /// <param name="enemy"></param>
    private void OnHit(AttributeSet enemy) {
        // BaseAbility.OnHit()
        // Modifiers.ForEach(x => x.OnHit(...));
    }

}
