using System;
using UnityEngine;
using Core.Abilities;
using Core.Abilities.Definitions;
using Core.Abilities.Instances;
using Core.Abilities.Structures;
using System.Linq;

[CreateAssetMenu(fileName = "StructureActionDefinition", menuName = "Ability System/Player Actions/Build Structure", order = 0)]
public class StructureActionDefinition : ActionDefinition {
    public StructureDefinition structure;

    public override void ActivateAction(AbilityManager owner, AbilityInstance ability, ActionInstance action, Vector3 target, Action<AttributeSet> OnHit = null) {
        base.ActivateAction(owner, ability, action, target, OnHit);
        // Check if the player has enough structures in stock
        // Spawn the structure.
        var obj = structure.SpawnStructure(target, owner, ability, action);
        var behaviour = obj.GetComponent<StructureBase>();
        float tickSpeed = 0;
        foreach (var item in ability.modifiers) {
            foreach (var mod in item.definition.PerAbilityModifier) {
                if (mod.Attribute == GameAttributes.StructureTickSpeed) {
                    tickSpeed += mod.value.GetValueAtLevel(item.level);
                }
            }
        }
        tickSpeed += owner.Attributes.StructureTickSpeed;
        float tickInterval = Formulas.StructureTickRateFormula(tickSpeed);
        obj.SetActive(true);
        behaviour.Activate(tickInterval, owner);
    }

}