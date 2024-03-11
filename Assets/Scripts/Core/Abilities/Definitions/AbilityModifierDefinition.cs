using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Abilities;
using Core.Abilities.Definitions;
using Core.Abilities.Enums;
using Core.Abilities.Instances;
using Core.AttributeSystem.Alignments;
using UnityEngine;

public abstract class AbilityModifierDefinition : ScriptableObject, IGetAlignmentLevel {
    // pass in the modifier instance so we can tell what level the mod is at
    public AlignmentDefinition alignment;

    [Tooltip("Stats which apply to the entire character.")]
    public List<StatModifier> GlobalStatModifier = new();

    [Tooltip("Stats which apply ONLY to this ability.")]
    public List<StatModifier> PerAbilityModifier = new();



    [Header("UI Properties")]
    public Sprite icon;
    public string ModifierName;
    [TextArea] public string Description;

    public abstract void OnActivate(AbilityManager owner, AbilityInstance ability, Vector3 target, ModifierInstance mod, Action<AttributeSet> OnHit = null);
    public abstract void OnHit(AbilityManager owner, AbilityInstance ability, ModifierInstance mod, AttributeSet hitTarget);
    public virtual string GetTooltipText(int level) {
        List<string> strings = new();
        GlobalStatModifier.ForEach(x => strings.Add(x.GetTooltipText(level)));
        PerAbilityModifier.ForEach(x => strings.Add(x.GetTooltipText(level)));
        if (alignment) strings.Add(alignment.GetTooltipText(level));
        strings.Add(Description);
        return string.Join("\n", strings);
    }

    public (AlignmentDefinition, int) GetAlignmentLevel(int level) {
        return (alignment, level); // modifiers don't have level multipliers?
    }
}