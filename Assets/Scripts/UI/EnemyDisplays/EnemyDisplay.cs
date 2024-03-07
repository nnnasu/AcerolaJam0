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
    public Image directionIndicator;


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
        var enemyPosition = MainCamera.WorldToScreenPoint(attributes.transform.position);
        Vector3 uiPosition = enemyPosition + offset;

        if (IsUIVisibleOnScreen(uiPosition)) {
            baseImage.transform.position = uiPosition;
            baseImage.gameObject.SetActive(true);
            directionIndicator.gameObject.SetActive(false);
            return;
        }

        baseImage.gameObject.SetActive(false);
        directionIndicator.gameObject.SetActive(true);
        // Set position to be within screen
        // vector from center of screen to current position
        Vector3 direction = uiPosition - new Vector3(Screen.width / 2, Screen.height / 2);
        float angle = Vector3.SignedAngle(Vector3.up, direction.normalized, Vector3.forward);
        directionIndicator.transform.eulerAngles = new Vector3(0, 0, angle);


        float rectWidth = directionIndicator.rectTransform.rect.width;
        float rectHeight = directionIndicator.rectTransform.rect.height;
        float x = Mathf.Clamp(uiPosition.x, rectWidth, Screen.width - rectWidth);
        float y = Mathf.Clamp(uiPosition.y, rectHeight, Screen.height - rectHeight);
        directionIndicator.transform.position = new Vector3(x, y); // todo change position
    }

    private bool IsUIVisibleOnScreen(Vector3 pos) {
        if (pos.x < 0 || pos.x > Screen.width) return false;
        if (pos.y < 0 || pos.y > Screen.height) return false;

        return true;
    }

}