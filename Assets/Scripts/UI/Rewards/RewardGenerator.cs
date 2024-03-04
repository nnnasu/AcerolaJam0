using System.Collections.Generic;
using System.Linq;
using Core.Abilities.Definitions;
using Core.Abilities.Instances;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardGenerator", menuName = "RewardGenerator", order = 0)]
public class RewardGenerator : ScriptableObject {
    public List<ActionDefinition> Actions = new();
    public List<ModifierDefinition> Modifiers = new();

    public List<ActionInstance> GetRandomActions(System.Random random, int number = 4) {
        return Actions
            .OrderBy(x => random.Next())
            .Take(number)
            .Select(x => new ActionInstance(x))
            .ToList();
    }

    public List<ModifierInstance> GetRandomModifiers(System.Random random, int number = 4) {
        return Modifiers
            .OrderBy(x => random.Next())
            .Take(number)
            .Select(x => new ModifierInstance(x))
            .ToList();
    }
}