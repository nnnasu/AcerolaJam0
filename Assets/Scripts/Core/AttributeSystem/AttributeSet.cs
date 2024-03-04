using System;
using System.Collections;
using System.Collections.Generic;
using Core.AttributeSystem;
using UnityEngine;

public class AttributeSet : MonoBehaviour {

    public float HP;
    public float MaxHP;

    public float MovementSpeedBase;
    public float MovementSpeedCurrent => MovementSpeedBase;
    public float AttackSpeed;
    public float BaseAttack;
    public EntityType entityType = EntityType.Enemy;

    public event Action<AttributeSet> OnDeath = delegate { };
    public event Action<float, float> OnHPChanged = delegate { };


    public virtual void ResetState() {
        HP = MaxHP;
    }

    private void OnEnable() {
        ResetState();
    }

    public virtual void TakeDamage(float amount) {
        float oldHP = HP;
        HP -= amount;
        OnHPChanged?.Invoke(oldHP, HP);
        if (HP <= 0) OnDeath?.Invoke(this);


    }



}
