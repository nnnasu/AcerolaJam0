using System;
using Core.Abilities.Structures;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.PositionDisplays {
    public class StructureDisplay : PositionDisplay {

        [Header("HP Bar")]
        public Image fillImage;
        private StructureAttributes attributes;
        public TextMeshProUGUI textDisplay;
        Tween HitTween;

        public override void Bind(DisplayTargetSource target, Camera mainCam) {
            base.Bind(target, mainCam);

            var attribute = target.GetComponent<StructureAttributes>();
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

        private void OnDeath(StructureAttributes attributeSet) {
            Unbind();
            Release();
        }

        private void OnHpChange(float oldValue, float newValue) {
            if (attributes == null) return;
            float maxHP = attributes.MaxHP;
            HitTween = Tween.Custom(oldValue / maxHP, newValue / maxHP, 0.5f, UpdateFillAmount);
            textDisplay.text = newValue.ToString("0");
        }

        private void UpdateFillAmount(float value) {
            fillImage.fillAmount = value;
        }

    }
}