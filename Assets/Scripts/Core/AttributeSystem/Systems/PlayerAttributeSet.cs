using System;
using System.Collections;
using System.Collections.Generic;
using Core.AttributeSystem;
using UnityEngine;


public class PlayerAttributeSet : AttributeSet {

    public float MP;
    public float MaxMP;
    public float MPRegenPercent;
    public float HPRegenPercent;
    public float StructureTickSpeed;
    public float CooldownReduction;

    public override void ResetState(bool resetHP = false) {
        base.ResetState(resetHP);
        if (baseAttributes is BasePlayerAttributes playerAttr) {
            if (resetHP) MP = playerAttr.MaxMP;
            MPRegenPercent = playerAttr.MPRegenPercent;
            HPRegenPercent = playerAttr.HPRegenPercent;
            StructureTickSpeed = playerAttr.StructureTickSpeed;
            CooldownReduction = playerAttr.CooldownReduction;
        }
    }

    public event Action<float, float> OnMPChanged = delegate { };

    public void CostMana(float amount) {
        float oldMP = MP;
        MP = Mathf.Clamp(MP - amount, 0, MaxMP);
        if (oldMP == MP) return;
        OnMPChanged?.Invoke(oldMP, MP);
    }
    public override void ApplyModifier(StatModifier modifier, int level, bool negate = false) {
        float value = modifier.value.GetValueAtLevel(level);
        if (negate) value *= -1;
        switch (modifier.Attribute) {
            case GameAttributes.MaxHP:
                MaxHP += value;
                break;
            case GameAttributes.MaxMP:
                MaxMP += value;
                break;
            case GameAttributes.MovementSpeed:
                MovementSpeed += value;
                break;
            case GameAttributes.AttackSpeed:
                AttackSpeed += value;
                break;
            case GameAttributes.BaseAttack:
                BaseAttack += value;
                break;
            case GameAttributes.MPRegenPercent:
                MPRegenPercent += value;
                break;
            case GameAttributes.HPRegenPercent:
                HPRegenPercent += value;
                break;
            case GameAttributes.StructureTickSpeed:
                StructureTickSpeed += value;
                break;
            case GameAttributes.CooldownReduction:
                CooldownReduction += value;
                break;
            default: break;
        }


    }
}