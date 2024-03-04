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
            if (resetHP) MaxMP = playerAttr.MaxMP;
            MPRegenPercent = playerAttr.MPRegenPercent;
            HPRegenPercent = playerAttr.HPRegenPercent;
            StructureTickSpeed = playerAttr.StructureTickSpeed;
            CooldownReduction = playerAttr.CooldownReduction;
        }
    }

    public event Action<float, float> OnMPChanged = delegate { };

    public void CostMana(float amount) {
        float oldMP = MP;
        MP -= amount;
        if (oldMP == MP) return;
        OnMPChanged?.Invoke(oldMP, MP);
    }

}