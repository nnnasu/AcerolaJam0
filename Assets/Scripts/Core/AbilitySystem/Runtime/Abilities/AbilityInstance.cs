using System;
using System.Collections;
using System.Collections.Generic;
using Core.AbilitySystem.Abilities;
using PrimeTween;
using UnityEngine;

[Serializable]
public class AbilityInstance {
    public AbilityTemplate BaseAbility;
    public AbilityManager owner;
    public int AbilityLevel = 1;
    public float cooldown = 1;
    public bool IsOnCooldown { get; private set; } = false;
    public Tween CooldownTween;

    public AbilityInstance(AbilityTemplate ability, AbilityManager owner) {
        BaseAbility = ability;
        this.owner = owner;
        cooldown = ability.BaseCooldown;
    }

    public bool ActivateAbility(Vector3 targetPosition) {
        if (IsOnCooldown) return false;

        float damage = BaseAbility.DamageScalingCurve.Evaluate(AbilityLevel);

        BaseAbility.Actions.ForEach(x => x.Execute(this, targetPosition, damage, null, OnHit));
        // Modifiers.ForEach(x => x.OnActivate(...));
        if (cooldown > 0) {
            IsOnCooldown = true;
            CooldownTween = Tween.Delay(cooldown, OnCooldownEnd);
        }
        return true;
    }

    private void OnCooldownEnd() {
        IsOnCooldown = false;
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
