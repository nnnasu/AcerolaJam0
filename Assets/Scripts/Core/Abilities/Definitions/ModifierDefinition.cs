using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using Core.Abilities.Definitions;
using Core.Abilities.Enums;
using Core.Abilities.Instances;
using UnityEngine;

// [CreateAssetMenu(fileName = "ModifierDefinition", menuName = "Modifiers/ModifierDefinition", order = 0)]
public abstract class ModifierDefinition : ScriptableObject {
    // pass in the modifier instance so we can tell what level the mod is at
    public AlignmentType alignment;

    [Tooltip("Stats which apply to the entire character.")]
    public List<StatModifier> GlobalStatModifier = new();

    [Tooltip("Stats which apply ONLY to this ability.")]
    public List<StatModifier> PerAbilityModifier = new();



    [Header("UI Properties")]
    public Sprite icon;
    public string ActionTitle;
    [TextArea] public string Description;

    public abstract void OnActivate(AbilityManager owner, AbilityInstance ability, Vector3 target, ModifierInstance mod, Action<AttributeSet> OnHit = null);
    public abstract void OnHit(AbilityManager owner, AbilityInstance ability, ModifierInstance mod, AttributeSet hitTarget);

}