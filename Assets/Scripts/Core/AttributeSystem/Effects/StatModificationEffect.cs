using Core.Utilities.Scaling;
using UnityEngine;
using System;
using System.Collections.Generic;


/// <summary>
/// This class encapsulates effects like debuffs which can be applied to enemies.
/// </summary>
[CreateAssetMenu(fileName = "StatBuff", menuName = "BuffEffects/Stat Modification", order = 0)]
public class StatModificationEffect : GameplayEffect {

    public List<StatModifier> modifiers = new();

    public override void Apply(AttributeSet attributeSet, int level) {
        foreach (var item in modifiers) {
            attributeSet.ApplyModifier(item, level);
        }
    }

    public override void Remove(AttributeSet attributeSet, int level) {
        foreach (var item in modifiers) {
            attributeSet.ApplyModifier(item, level, true);
        }
    }
}