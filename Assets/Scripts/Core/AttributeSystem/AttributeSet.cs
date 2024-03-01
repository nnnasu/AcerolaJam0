using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeSet : MonoBehaviour {

    public float HP;
    public float MaxHP;

    public float MovementSpeedBase;
    public float MovementSpeedCurrent;

    public int AttackSpeed;

    public event Action<AttributeSet> OnDeath = delegate { };

    public void ResetState() {
        HP = MaxHP;

    }

    public void TakeDamage(float amount) {
        HP -= amount;
        if (HP <= 0) OnDeath?.Invoke(this);
    }



    public float GetTargetAttackTime(float BAT) {
        float attackTime = BAT / (1 + AttackSpeed / 15);
        return Mathf.Clamp(attackTime, 0.2f, 6); // multiply animation speed to reach this amount.
    }


}
