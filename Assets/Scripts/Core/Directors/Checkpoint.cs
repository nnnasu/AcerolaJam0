using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using UnityEngine;

public class Checkpoint : PoolableBehaviour {
    public EventChannel OnEnter;

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<AbilityManager>()) {
            // player.
            OnEnter.RaiseEvent();
            ReturnToPool();
        }
    }
}
