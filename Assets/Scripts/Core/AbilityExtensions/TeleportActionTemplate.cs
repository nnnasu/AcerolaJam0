using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Teleport Action", menuName = "Action/Teleport", order = 0)]
public class TeleportActionTemplate : ActionTemplateBase {



    public override void Execute(AbilityInstance instance, AbilityManager owner, Vector3 target, float damage = 0, Action<AttributeSet> OnHit = null) {
        Vector3 direction = target - owner.transform.position;
        direction.y = 0;
        float mag = Mathf.Clamp(direction.magnitude, 0, TargetRange);
        direction = direction.normalized * mag;
        Vector3 targetPoint = owner.transform.position + direction;

        var charcon = owner.GetComponent<CharacterController>();
        charcon.enabled = false;
        owner.transform.position = targetPoint;
        charcon.enabled = true;
    }
}
