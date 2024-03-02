using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class AbilityActionInstance {
    public ActionTemplateBase action;

    public float baseCooldown => action.BaseCooldown;
    public float currentCost;
    public float usageTime => action.UsageTime;

    public AbilityActionInstance(ActionTemplateBase action) {
        this.action = action;
        
    }

    public float CalculateDamage(AbilityInstance ability, AbilityManager owner) {
        // Calculate based on owner and active modifiers on ability.
        return 1;
    }

    

    public void Execute(AbilityInstance ability, AbilityManager owner, Vector3 target, Action<AttributeSet> OnHit = null) {
        float damage = CalculateDamage(ability, owner);
        action.Execute(ability, owner, target, damage, OnHit);
    }


}
