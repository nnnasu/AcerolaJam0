using System;
using System.Collections;
using System.Collections.Generic;
using Core.AbilitySystem.Abilities;
using Core.AbilitySystem.Alignments;
using UnityEngine;


// Disallow multiple basic attack, Summon, Movement
public enum ActionType {
    BasicAttack,
    Projectile,
    PointAOE,
    Summon,
    Movement
}


public abstract class ActionTemplateBase : ScriptableObject {

    [Header("Ability Parameters")]
    public ActionType ActionType;
    public float DamageMultiplier = 1; // base atk scaling
    public float BaseCooldown = 1;
    public float BaseMPCost = 0;
    public AberrantType Alignment = AberrantType.Neutral;

    public float UsageTime = 0; // While this is active, the ability is locked out.


    [Header("Target Type")]
    public TargetingType targetingType;
    [Tooltip("Size of the target display when using this action.")]
    public Vector3 TargetDisplay = new(1, 2, 3);
    [Tooltip("Position of the pivot display relative to the user.")]
    public Vector3 TargetPivotOffset = new(0, 0, 0.5f);
    [Tooltip("Clamps target displays to this range within the player. ")]
    public float TargetRange = 5;

    [Header("UI Properties")]
    public Texture2D icon;
    public string ActionTitle;
    [TextArea] public string Description;

    public abstract void Execute(AbilityInstance instance, AbilityManager owner, Vector3 target, float damage = 0, Action<AttributeSet> OnHit = null);

}