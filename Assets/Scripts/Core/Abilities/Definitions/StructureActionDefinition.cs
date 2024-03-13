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

    protected override void ActivateActionImplementation(AbilityManager owner, AbilityInstance ability, ActionInstance action, Vector3 target, Action<AttributeSet> OnHit = null) {
        // Check if the player has enough structures in stock
        if (!owner.StructureStorage.ContainsKey(structure)) {
            return; // This should never happen.
        }
        var store = owner.StructureStorage[structure];
        if (store.CurrentCharges <= 0) return;

        // Spawn the structure.                
        var obj = store.GetStructure();
        if (!obj) return;

        float tickSpeed = 0;
        foreach (var item in ability.modifiers) {
            foreach (var mod in item.definition.PerAbilityModifier) {
                if (mod.Attribute == GameAttributes.StructureTickSpeed) {
                    tickSpeed += mod.value.GetValueAtLevel(item.level);
                }
            }
        }

        // TODO: Set HP, etc. on the structure
        tickSpeed += owner.Attributes.StructureTickSpeed;
        float tickInterval = Formulas.StructureTickRateFormula(tickSpeed);
        obj.gameObject.SetActive(true);
        obj.Definition = structure;
        obj.transform.position = target;

        obj.Activate(tickInterval, owner, store);
    }

}