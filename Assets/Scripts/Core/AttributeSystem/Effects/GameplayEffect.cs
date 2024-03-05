using System.Collections;
using System.Collections.Generic;
using Core.Utilities.Scaling;
using UnityEngine;

/// <summary>
/// This class encapsulates effects like debuffs which can be applied to enemies.
/// </summary>
public abstract class GameplayEffect : ScriptableObject {

    public ScaledFloat duration;
    public EffectType effectType = EffectType.Duration;


    [Header("UI Properties")]
    public Sprite icon;
    public string EffectName;
    [TextArea] public string Description;

    public abstract void Apply(AttributeSet attributeSet, int level);
    public abstract void Remove(AttributeSet attributeSet, int level);

}