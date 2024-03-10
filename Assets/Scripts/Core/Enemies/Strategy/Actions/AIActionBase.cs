using System.Collections.Generic;
using Core.Enemies;
using Core.Enemies.Conditions;
using Core.Enemies.Strategy;
using UnityEngine;

public abstract class AIActionBase : ScriptableObject {

    public List<ConditionBase> Conditions = new();

    public abstract float Execute(AIController controller, AIPackage package, Vector3? playerPosition);

}