using System.Collections;
using System.Collections.Generic;
using Core.Utilities.Scaling;
using UnityEngine;

/// <summary>
/// This class encapsulates effects like debuffs which can be applied to enemies.
/// </summary>
// [CreateAssetMenu(fileName = "GameplayEffect", menuName = "GameplayEffect", order = 0)]
public abstract class GameplayEffect : ScriptableObject {

    public ScaledFloat duration;
    public EffectType effectType = EffectType.Duration;

    public abstract void Apply(AttributeSet attributeSet, int level);
    public abstract void Remove(AttributeSet attributeSet, int level);

}