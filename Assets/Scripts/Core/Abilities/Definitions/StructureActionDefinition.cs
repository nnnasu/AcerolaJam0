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
        structure.SpawnStructure(target, owner, ability, action);

    }

    public override void OnHit(AbilityManager owner, AbilityInstance ability, ActionInstance action, AttributeSet target) {
        // Doesn't do anything, hits aren't passed from structure to player.        
    }
}