using System;
using UnityEngine;
using Core.Abilities;
using Core.Abilities.Definitions;
using Core.Abilities.Instances;

[CreateAssetMenu(fileName = "StructureActionDefinition", menuName = "Player Actions/Build Structure", order = 0)]
public class StructureActionDefinition : ActionDefinition {
    public StructureDefinition structure;

    public override void ActivateAction(AbilityManager owner, AbilityInstance ability, ActionInstance action, Vector3 target, Action<AttributeSet> OnHit = null) {
        base.ActivateAction(owner, ability, action, target, OnHit);
        // Check if the player has enough structures in stock
        // Spawn the structure.
        var obj = structure.SpawnStructure(target, owner, ability, action);
        obj.SetActive(true);
        

    }

}