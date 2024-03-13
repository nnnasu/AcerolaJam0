using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ValueBar : MonoBehaviour {
    public Image fill;
    public Image ghostFill;
    public TextMeshProUGUI text;

    public float MaxValue;
    public float HitSpeed = 0.3f;
    public float ghostSpeed = 1f;
    public float ghostDelay = 1f;
    Tween HitTween;
    Tween GhostTween;

    public void OnValueChanged(float oldValue, float newValue, float maxValue) {
        text.text = $"{newValue:0}/{maxValue:0}";
        MaxValue = maxValue;
        HitTween.Stop();
        GhostTween.Stop();
        HitTween = Tween.Custom(oldValue / MaxValue, newValue / MaxValue, HitSpeed, UpdateFillAmount);
        GhostTween = Tween.Custom(oldValue / MaxValue, newValue / MaxValue, ghostSpeed, UpdateGhostAmount, startDelay: 2);
    }

    private void UpdateFillAmount(float value) {
        fill.fillAmount = value;
    }

    private void UpdateGhostAmount(float value) {
        ghostFill.fillAmount = value;
    }

    
}
