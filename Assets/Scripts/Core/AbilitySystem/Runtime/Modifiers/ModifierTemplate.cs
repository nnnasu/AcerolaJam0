using System;
using System.Collections;
using System.Collections.Generic;
using Core.Utilities.Scaling;
using UnityEngine;


[Serializable]
public class StatModifier {
    public Attributes Attribute;
    public ScaledFloat value;
}

[Serializable]
public class AbilityModifier {
    public AbilityAttributes Attribute;
    public ScaledFloat value;
}

[CreateAssetMenu(fileName = "ModifierTemplate", menuName = "ModifierTemplate", order = 0)]
public abstract class ModifierTemplate : ScriptableObject {
    [Header("Flat Stat Modifications")]
    public List<StatModifier> FlatStats = new();
    public List<AbilityModifier> AbilityModifiers = new();
    public abstract void OnAbilityActionUsed(AbilityInstance ability, AbilityAction action, AbilityManager owner);


}

