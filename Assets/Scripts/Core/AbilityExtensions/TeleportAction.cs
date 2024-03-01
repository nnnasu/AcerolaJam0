using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAction : AbilityAction {

    public float Range = 5;

    public override void Execute(AbilityInstance ability, Vector3 target, float damage = 0, GameplayEffect effect = null, Action<AttributeSet> OnHit = null) {
        Vector3 direction = target - ability.owner.transform.position;
        direction.y = 0;
        float mag = Mathf.Clamp(direction.magnitude, 0, Range);
        direction = direction.normalized * mag;
        Vector3 targetPoint = ability.owner.transform.position + direction;

        var charcon = ability.owner.GetComponent<CharacterController>();
        charcon.enabled = false;
        ability.owner.transform.position = targetPoint;
        charcon.enabled = true;
    }

}
