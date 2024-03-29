using System;
using System.Collections;
using System.Collections.Generic;
using Core.AttributeSystem;
using Core.AttributeSystem.Alignments;
using UnityEngine;
using UnityEngine.Events;


public class PlayerAttributeSet : AttributeSet {

    public float MP;
    public float MaxMP;
    public float MPRegenPercent;
    public float HPRegenPercent;
    public float MPCostReduction;
    public float StructureTickSpeed;
    public float StructureReboundBonus = 0;
    public float StructureHPBonus = 0;
    public float CooldownReduction;
    public UnityEvent PlayerDeath;

    public Dictionary<AlignmentDefinition, int> levels = new();

    public override void ResetState(bool resetHP = false) {
        base.ResetState(resetHP);
        if (baseAttributes is BasePlayerAttributes playerAttr) {
            MaxMP = playerAttr.MaxMP;
            if (resetHP) MP = playerAttr.MaxMP;
            MPRegenPercent = playerAttr.MPRegenPercent;
            HPRegenPercent = playerAttr.HPRegenPercent;
            StructureTickSpeed = playerAttr.StructureTickSpeed;
            CooldownReduction = playerAttr.CooldownReduction;
            MPCostReduction = playerAttr.MPCostReduction;
        }
    }

    public event Action<float, float> OnMPChanged = delegate { };

    private void OnEnable() {
        OnDeath += die;
    }
    private void OnDisable() {
        OnDeath -= die;
    }

    private void die(AttributeSet attributeSet) {
        PlayerDeath?.Invoke();
    }


    public void CostMana(float amount) {
        float oldMP = MP;
        MP = Mathf.Clamp(MP - amount, 0, MaxMP);
        if (oldMP == MP) return;
        OnMPChanged?.Invoke(oldMP, MP);
    }

    public void UpdateAlignmentStatus() {
        foreach (var item in levels) {
            var alignment = item.Key;
            int level = item.Value;
            alignment.ApplyEffects(this, level);
        }
    }


    public void AddAlignmentLevels(AlignmentDefinition alignment, int level) {

        if (alignment == null) return;
        if (!levels.ContainsKey(alignment)) {
            levels.Add(alignment, 0);
        }
        levels[alignment] += level;
    }

    public override void ApplyModifier(StatModifier modifier, int level, bool negate = false, int stacks = 1) {
        float rawValue = modifier.GetValue(level, stacks);
        float value = rawValue;
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
            case GameAttributes.DamageTaken:
                if (negate) DamageTakenMult /= rawValue;
                else DamageTakenMult *= rawValue;
                break;
            case GameAttributes.DamageDealt:
                if (negate) DamageDealtMult /= rawValue;
                else DamageDealtMult *= rawValue;
                break;
            case GameAttributes.StructureReboundDamage:
                StructureReboundBonus += value;
                break;
            case GameAttributes.StructureHPBonus:
                StructureReboundBonus += value;
                break;
            default: break;
        }

    }
}