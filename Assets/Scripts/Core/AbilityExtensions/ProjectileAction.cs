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

    public override void Execute(AbilityManager manager, Vector3 target, float damage = 0, GameplayEffect effect = null, Action<AttributeSet> OnHit = null) {
        Vector3 direction = target - manager.transform.position;
        direction.y = 0;
        direction.Normalize();
        Projectile obj = GameObject.Instantiate(PrefabToFire).GetComponent<Projectile>();
        obj.transform.position = manager.transform.position;
        if (useCurve) {
            obj.Activate(curve, duration, direction, OnHit);
        } else obj.Activate(speed, duration, direction, OnHit);

        obj.OnReturn += DestroyPrefab;
    }


    private void DestroyPrefab(GameObject obj) {
        obj.GetComponent<Projectile>().OnReturn -= DestroyPrefab;
        GameObject.Destroy(obj);
    }
}
