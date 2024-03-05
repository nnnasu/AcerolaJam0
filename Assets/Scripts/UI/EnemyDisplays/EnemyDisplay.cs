using System;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDisplay : PoolableBehaviour {

    private AttributeSet attributes;

    [Header("UI Stuff")]
    public Image baseImage;
    public Image fillImage;
    public TextMeshProUGUI textDisplay;

    public Vector3 offset;

    public event Action<EnemyDisplay> OnRelease = delegate { };
    Tween HitTween;

    Camera MainCamera;

    public void Bind(AttributeSet attribute, Camera mainCam) {
        attributes = attribute;
        MainCamera = mainCam;
        attributes.OnDeath += OnDeath;
        attributes.OnHPChanged += OnHpChange;

        OnHpChange(attributes.HP, attributes.HP);
    }

    private void Unbind() {
        attributes.OnDeath -= OnDeath;
        attributes.OnHPChanged -= OnHpChange;
        attributes = null;
    }

    private void OnDeath(AttributeSet attributeSet) {
        Unbind();
        OnRelease?.Invoke(this);
        ReturnToPool();
    }
    private void OnHpChange(float oldValue, float newValue) {
        float maxHP = attributes.MaxHP;
        HitTween = Tween.Custom(oldValue / maxHP, newValue / maxHP, 0.5f, UpdateFillAmount);
        textDisplay.text = newValue.ToString("0"); 
    }

    private void UpdateFillAmount(float value) {
        fillImage.fillAmount = value;
    }


    public void Tick() {
        var pos = MainCamera.WorldToScreenPoint(attributes.transform.position);
        baseImage.transform.position = pos + offset;

    }

}