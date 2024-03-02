using System;
using System.Collections;
using System.Collections.Generic;
using Core.AbilitySystem.Abilities;
using PrimeTween;
using UnityEngine;

[Serializable]
public class AbilityInstance {

    [HideInInspector]
    public List<AbilityActionInstance> Actions = new(4);
    public List<ModifierInstance> Modifiers = new(4);
    public int AbilityLevel = 1;
    public bool IsOnCooldown { get; private set; } = false;

    public float cooldown { get; private set; } = 1;
    public float usageTime { get; private set; } = 0;
    public Tween CooldownTween;
    public Tween UsageTween;

    public AbilityInstance(AbilityTemplate template) {
        template.ActionTemplates.ForEach(x => Actions.Add(new(x)));        
    }


    public bool ActivateAbility(AbilityManager owner, Vector3 targetPosition) {
        if (IsOnCooldown) return false;
        RecalculateCooldownTime(owner);        
        foreach (var item in Actions) {
            item.Execute(this, owner, targetPosition, OnHit);
        }
        foreach (var item in Modifiers) {
            item.OnActivated(this, owner, targetPosition);
        }

        IsOnCooldown = true;
        if (usageTime <= float.Epsilon) StartCooldown();
        else UsageTween = Tween.Delay(usageTime, StartCooldown);

        return true;
    }

    private void StartCooldown() {
        CooldownTween = Tween.Delay(cooldown, OnCooldownEnd);
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

    public float RecalculateUsageTime() {
        float usage = 0;
        foreach (var item in Actions) {
            if (item == null) break;
            usage = Mathf.Max(usage, item.usageTime);
        }
        usageTime = usage;
        return usage;
    }

    public float RecalculateCooldownTime(AbilityManager owner) {
        float cdr = 0;
        foreach (var item in Modifiers) {
            foreach (var mod in item.source.AbilityModifiers) {
                if (mod.Attribute == AbilityAttributes.CooldownReduction) cdr += mod.value.GetValueAtLevel(item.level);
            }
        }
        cdr += owner.Attributes.CooldownReduction;

        float sum = 0;
        foreach (var item in Actions) {
            sum += item.baseCooldown;
        }

        cooldown = Formulas.CooldownReductionFormula(sum, cdr);
        return cooldown;
    }



}
