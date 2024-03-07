using Core.Utilities.Scaling;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;


/// <summary>
/// This class encapsulates effects like debuffs which can be applied to enemies.
/// </summary>
[CreateAssetMenu(fileName = "StatBuff", menuName = "Status Effects/Stat Modification", order = 0)]
public class StatModificationEffect : StatusEffect {

    public List<StatModifier> modifiers = new();

    public override void Apply(AttributeSet attributeSet, EffectInstance instance) {
        foreach (var item in modifiers) {
            attributeSet.ApplyModifier(item, instance.level);
        }
    }

    public override string GetDescription(EffectInstance instance) {
        var ls = modifiers.Select(x => x.GetTooltipText(instance.level)).ToList();
        return string.Join("\n", ls);
    }

    public override void Remove(AttributeSet attributeSet, EffectInstance instance) {
        foreach (var item in modifiers) {
            attributeSet.ApplyModifier(item, instance.level, true);
        }
    }
}