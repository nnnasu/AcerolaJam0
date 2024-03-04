using Core.Enemies;
using Core.Enemies.Strategy;
using UnityEngine;

public abstract class AIActionBase : ScriptableObject {

    public abstract float Execute(AIController controller, AIPackage package, Vector3? playerPosition);
    
}