using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class encapsulates effects like debuffs which can be applied to enemies.
/// </summary>
// [CreateAssetMenu(fileName = "GameplayEffect", menuName = "GameplayEffect", order = 0)]
public abstract class GameplayEffect : ScriptableObject {

    public abstract void Apply(AttributeSet attributeSet);

}