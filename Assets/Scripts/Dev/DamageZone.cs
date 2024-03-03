using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour {
    public float damage = 5;
    private void OnTriggerEnter(Collider other) {
        var attributes = other.GetComponent<AttributeSet>();
        if (!attributes) return;

        attributes.TakeDamage(damage);
    }
}
