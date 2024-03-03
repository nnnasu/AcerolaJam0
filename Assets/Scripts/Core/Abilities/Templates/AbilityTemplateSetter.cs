using System.Linq;
using Core.Abilities;
using Core.Abilities.Instances;
using Core.Abilities.Templates;
using UnityEngine;

public class AbilityTemplateSetter : MonoBehaviour {
    public AbilityTemplate BasicAttackTemplate;
    public AbilityTemplate[] AbilitiesToAdd;
    public AbilityManager target;


    private void Awake() {
        target.BasicAttack = new(target);
        target.BasicAttack = CreateInstance(target, BasicAttackTemplate);
        target.Abilities.Clear();

        for (int i = 0; i < 4; i++) {
            if (i < AbilitiesToAdd.Length) {
                target.Abilities.Add(CreateInstance(target, AbilitiesToAdd[i]));
            } else {
                target.Abilities.Add(new(target));
            }
        }
        target.RecalculateStats();
    }

    private AbilityInstance CreateInstance(AbilityManager manager, AbilityTemplate template) {
        AbilityInstance result = new(manager);
        template.actions.ForEach(x => result.actions.Add(new(x.action) { level = x.level }));
        template.modifiers.ForEach(x => result.modifiers.Add(new(x.mod) { level = x.level }));
        return result;
    }

}