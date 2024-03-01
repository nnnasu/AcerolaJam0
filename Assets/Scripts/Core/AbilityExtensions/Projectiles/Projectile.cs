using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour, IPoolable {

    public bool DestroyOnContact;
    public event Action<GameObject> OnReturn = delegate { };
    public float damage = 0;
    public GameplayEffect effect = null;
    Tween ExpiryTween;
    AnimationCurve curve = null;
    float speed = 0;
    bool useCurve = false;
    Action<AttributeSet> OnHitAction = null;

    IObjectPool<GameObject> poolRef;
    public IObjectPool<GameObject> Pool { get => poolRef; set => poolRef = value; }

    private void OnTriggerEnter(Collider other) {
        if (OnHitAction != null) OnHitAction?.Invoke(null);
    }

    public void OnExpiry() {
        useCurve = false;
        speed = 0;
        curve = null;
        OnHitAction = null;
        ExpiryTween.Stop();
        ReturnToPool();
    }

    private void Update() {
        if (useCurve) {
            speed = curve.Evaluate(ExpiryTween.elapsedTime);
        }
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void Activate(float duration, Vector3 directionWS, Action<AttributeSet> OnHitAction = null) {
        gameObject.SetActive(true);
        ExpiryTween = Tween.Delay(duration, OnExpiry);
        transform.rotation = Quaternion.LookRotation(directionWS);
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

    public void SetReferenceToPool(IObjectPool<GameObject> pool) {
        poolRef = pool;
    }

    public void ReturnToPool() {
        Pool?.Release(gameObject);
    }
}
