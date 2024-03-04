using System;
using UnityEngine;
using Core.Abilities;
using Core.Abilities.Definitions;
using Core.Abilities.Instances;

[CreateAssetMenu(fileName = "StructureActionDefinition", menuName = "Actions/Structure Action", order = 0)]
public class StructureActionDefinition : ActionDefinition {
    public StructureDefinition structure;

    public override void ActivateAbility(AbilityManager owner, AbilityInstance ability, ActionInstance action, Vector3 target, Action<AttributeSet> OnHit = null) {
        // Check if the player has enough structures in stock
        // Spawn the structure.
        var obj = structure.SpawnStructure(target, owner, ability, action);
        obj.SetActive(true);
        

    }

}