using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;

public class Projectile : MonoBehaviour {

    public bool DestroyOnContact;
    public event Action<GameObject> OnReturn = delegate { };
    public float damage = 0;
    public GameplayEffect effect = null;
    Tween ReturnTween;
    AnimationCurve curve = null;
    float speed = 0;
    bool useCurve = false;
    bool isActive = false;
    Action<AttributeSet> OnHitAction = null;


    private void OnTriggerEnter(Collider other) {
        if (OnHitAction != null) OnHitAction?.Invoke(null);

    }

    public void ReturnProjectileToPool() {
        enabled = false;
        useCurve = false;
        speed = 0;
        curve = null;
        OnHitAction = null;
        ReturnTween.Stop();
        OnReturn?.Invoke(gameObject);
    }

    private void Update() {
        if (!isActive) return;
        if (useCurve) {
            speed = curve.Evaluate(ReturnTween.elapsedTime);
        }
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void Activate(float duration, Vector3 directionWS, Action<AttributeSet> OnHitAction = null) {
        enabled = true;
        ReturnTween = Tween.Delay(duration, ReturnProjectileToPool);
        transform.rotation = Quaternion.LookRotation(directionWS);
        isActive = true;
    }
    public void Activate(AnimationCurve speedCurve, float duration, Vector3 directionWS, Action<AttributeSet> OnHitAction = null) {
        Activate(duration, directionWS);
        speed = speedCurve.Evaluate(0);
        useCurve = true;
    }

    public void Activate(float speed, float duration, Vector3 directionWS, Action<AttributeSet> OnHitAction = null) {
        Activate(duration, directionWS);
        this.speed = speed;
        useCurve = false;
    }


}
