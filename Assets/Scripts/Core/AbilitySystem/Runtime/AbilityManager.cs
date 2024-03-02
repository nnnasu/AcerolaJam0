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

    public PlayerAttributeSet Attributes;

    private void Start() {
        BasicAttack = new(BasicAttackTemplate);
        
    }


    public void OnClick(Vector3 position) {
        BasicAttack.ActivateAbility(this, position);
        
    }

    public event Action<AbilityInstance> OnAbilityUsed = delegate { };

}
