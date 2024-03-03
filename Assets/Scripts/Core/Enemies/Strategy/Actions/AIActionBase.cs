using Core.Enemies;
using Core.Enemies.Strategy;
using UnityEngine;

// [CreateAssetMenu(fileName = "AIActionBase", menuName = "AIActionBase", order = 0)]
public abstract class AIActionBase : ScriptableObject {

    public abstract float Execute(AIController controller, AIPackage package, Vector3? playerPosition);
    
}