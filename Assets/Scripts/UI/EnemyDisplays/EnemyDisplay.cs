using System;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.PositionDisplays {
    public class EnemyDisplay : PositionDisplay {

        [Header("Enemy HP Bar")]
        public Image fillImage;
        private AttributeSet attributes;
        public TextMeshProUGUI textDisplay;
        Tween HitTween;

        public override void Bind(DisplayTargetSource target, Camera mainCam) {
            base.Bind(target, mainCam);

            var attribute = target.GetComponent<AttributeSet>();
            attributes = attribute;

            attributes.OnDeath += OnDeath;
            attributes.OnHPChanged += OnHpChange;

            OnHpChange(attributes.HP, attributes.HP);
        }

        private void Unbind() {
            HitTween.Complete();
            attributes.OnDeath -= OnDeath;
            attributes.OnHPChanged -= OnHpChange;
            attributes = null;
        }

        private void OnDeath(AttributeSet attributeSet) {
            Unbind();
            Release();
        }

        private void OnHpChange(float oldValue, float newValue) {
            float maxHP = attributes.MaxHP;
            HitTween = Tween.Custom(oldValue / maxHP, newValue / maxHP, 0.5f, UpdateFillAmount);
            textDisplay.text = newValue.ToString("0");
        }

        private void UpdateFillAmount(float value) {
            fillImage.fillAmount = value;
        }

    }
}