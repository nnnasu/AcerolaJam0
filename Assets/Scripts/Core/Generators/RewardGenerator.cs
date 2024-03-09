using System.Collections.Generic;
using System.Linq;
using Core.Abilities.Definitions;
using Core.Abilities.Instances;
using KaimiraGames;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardGenerator", menuName = "Game Management/RewardGenerator", order = 0)]
public class RewardGenerator : ScriptableObject {
    public List<ActionDefinition> Actions = new();
    public List<AbilityModifierDefinition> Modifiers = new();

    public WeightedList<ActionDefinition> ActionWeights;
    public WeightedList<AbilityModifierDefinition> ModifierWeights;
    private void OnEnable() {
        if (ActionWeights == null) {
            List<WeightedListItem<ActionDefinition>> weights
                = Actions
                    .Select(x => new WeightedListItem<ActionDefinition>(x, 1))
                    .ToList();
            ActionWeights = new(weights);
        }

        if (ModifierWeights == null) {
            List<WeightedListItem<AbilityModifierDefinition>> weights
                = Modifiers
                    .Select(x => new WeightedListItem<AbilityModifierDefinition>(x, 1))
                    .ToList();
            ModifierWeights = new(weights);
        }

    }

    public List<ActionInstance> GetRandomActions(int number = 4) {
        List<ActionInstance> result = new();
        for (int i = 0; i < number; i++) {
            result.Add(new ActionInstance(ActionWeights.Next()));
        }
        return result;
    }

    public List<ModifierInstance> GetRandomModifiers(int number = 4) {
        List<ModifierInstance> result = new();
        for (int i = 0; i < number; i++) {
            result.Add(new ModifierInstance(ModifierWeights.Next()));
        }
        return result;
    }
}