using System;
using System.Collections;
using System.Collections.Generic;
using Core.AttributeSystem;
using UnityEngine;

public class AttributeSet : MonoBehaviour {

    public BaseAttributes baseAttributes;

    public float HP;
    public float MaxHP;
    public float MovementSpeed;
    public float AttackSpeed;
    public float BaseAttack;
    public EntityType entityType = EntityType.Enemy;

    public event Action<AttributeSet> OnDeath = delegate { };
    public event Action<float, float> OnHPChanged = delegate { };


    public virtual void ResetState(bool resetHP = false) {
        MaxHP = baseAttributes.MaxHP;
        if (resetHP) HP = MaxHP;
        MovementSpeed = baseAttributes.MovementSpeedBase;
        AttackSpeed = baseAttributes.AttackSpeed;
        BaseAttack = baseAttributes.BaseAttack;

    }

    private void OnEnable() {
        ResetState(true);
    }

    public virtual void TakeDamage(float amount) {
        float oldHP = HP;
        HP -= amount;
        OnHPChanged?.Invoke(oldHP, HP);
        if (HP <= 0) OnDeath?.Invoke(this);
    }



}
