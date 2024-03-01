using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fires an object towards the given point, starting from the player's position
/// </summary>
[Serializable]
public class ProjectileAction : AbilityAction {
    public GameObject PrefabToFire;
    public float duration;
    public float speed;
    public bool useCurve;
    public AnimationCurve curve;

    public override void Execute(AbilityInstance instance, Vector3 target, float damage = 0, GameplayEffect effect = null, Action<AttributeSet> OnHit = null) {
        Vector3 direction = target - instance.owner.transform.position;
        direction.y = 0;
        direction.Normalize();
        var poolObj = GlobalPool.Current.GetObject(PrefabToFire);
        Projectile obj = poolObj.GetComponent<Projectile>();
        if (obj == null) return;

        obj.transform.position = instance.owner.transform.position;
        if (useCurve) {
            obj.Activate(curve, duration, direction, OnHit);
        } else obj.Activate(speed, duration, direction, OnHit);
        
    }


}
