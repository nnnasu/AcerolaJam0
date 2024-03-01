using System.Collections;
using System.Collections.Generic;
using Core.AbilitySystem.Abilities;
using UnityEngine;

public class AbilityManager : MonoBehaviour {

    [SerializeField] AbilityTemplate action;

    public void OnClick(Vector3 position) {
        action.Actions.ForEach(x => x.Execute(this, position, 0, null));
    }

    
}
