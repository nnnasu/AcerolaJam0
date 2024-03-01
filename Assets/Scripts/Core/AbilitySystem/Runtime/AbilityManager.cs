using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.AbilitySystem.Abilities;
using UnityEngine;

public class AbilityManager : MonoBehaviour {

    [SerializeField] AbilityTemplate[] Loadout;
    [SerializeField] AbilityTemplate BasicAttackTemplate;

    AbilityInstance BasicAttack;
    List<AbilityInstance> Abilities = new();

    public AttributeSet Attributes;

    private void Start() {
        foreach (var item in Loadout) {
            Abilities.Add(new AbilityInstance(item, this));
        }
        BasicAttack = new(BasicAttackTemplate, this);
    }


    public void OnClick(Vector3 position) {
        BasicAttack?.ActivateAbility(position);
    }

    public event Action<AbilityInstance> OnAbilityUsed = delegate { };

}
