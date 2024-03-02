using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeSet : MonoBehaviour {

    public float HP;
    public float MaxHP;

    public float MovementSpeedBase;
    public float MovementSpeedCurrent;
    public float AttackSpeed;

    public event Action<AttributeSet> OnDeath = delegate { };

    public virtual void ResetState() {
        HP = MaxHP;

    }

    public virtual void TakeDamage(float amount) {
        HP -= amount;
        if (HP <= 0) OnDeath?.Invoke(this);
    }

}
