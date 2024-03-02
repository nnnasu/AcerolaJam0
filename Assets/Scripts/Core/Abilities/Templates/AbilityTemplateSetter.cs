using Core.Abilities;
using Core.Abilities.Instances;
using Core.Abilities.Templates;
using UnityEngine;

public class AbilityTemplateSetter : MonoBehaviour {
    public AbilityTemplate BasicAttackTemplate;
    public AbilityTemplate[] Abilities;

    public AbilityManager target;


    private void Start() {
        target.BasicAttack = new(target);
        AddToInstance(target.BasicAttack, BasicAttackTemplate);
        target.Abilities.Clear();
        foreach (var item in Abilities) {
            AbilityInstance instance = new(target);
            AddToInstance(instance, item);
        }
        target.RecalculateStats();

    }

    private void AddToInstance(AbilityInstance instance, AbilityTemplate template) {
        instance.actions.Clear();
        instance.modifiers.Clear();

        template.actions.ForEach(x => instance.actions.Add(new(x.action) { level = x.level }));
        template.modifiers.ForEach(x => instance.modifiers.Add(new(x.mod) { level = x.level }));
    }
}