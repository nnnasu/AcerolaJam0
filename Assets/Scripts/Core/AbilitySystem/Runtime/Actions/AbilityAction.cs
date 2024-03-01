using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public abstract class AbilityAction {

    /// <summary>
    /// Executes this action.
    /// </summary>
    /// <param name="manager">The player's ability manager</param>
    /// <param name="target">The target point clicked in world-space.</param>
    public abstract void Execute(AbilityManager manager, Vector3 target, float damage, GameplayEffect effect);
}
