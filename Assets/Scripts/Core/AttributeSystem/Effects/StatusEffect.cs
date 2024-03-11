using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.AttributeSystem.Conditions;
using Core.Utilities.Scaling;
using UnityEngine;

/// <summary>
/// This class encapsulates effects like debuffs which can be applied to enemies.
/// </summary>
public abstract class StatusEffect : ScriptableObject {

    public ScaledFloat duration;
    public EffectType effectType = EffectType.Duration;
    public List<TargetCondition> Conditions = new();


    [Header("UI Properties")]
    public Sprite icon;
    public string EffectName;
    public abstract string GetDescription(EffectInstance instance);

    public bool ShouldDisplayNumber = false;
    public bool ShowInHUD = true;

    /// <summary>
    /// For subclasses to implement what their number should be. This could be stacks, effect amounts, etc.
    /// </summary>
    /// <returns></returns>
    public virtual int GetNumberToDisplay(EffectInstance effectInstance) {
        return 0;
    }

    public abstract void Apply(AttributeSet attributeSet, EffectInstance instance);
    public abstract void Remove(AttributeSet attributeSet, EffectInstance instance);

    public virtual EffectInstance GetEffectInstance(AttributeSet target, int level) {
        return new(this, level);
    }

    public virtual bool CanApplyEffect(AttributeSet target, EffectInstance instance) {
        return Conditions.All(x => x.TestCondition(target));
    }
}