using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillDisplay : MonoBehaviour {
    public Image Border;
    public Image MainIcon;
    public Image CooldownOverlay;
    public TextMeshProUGUI CooldownText;
    public TextMeshProUGUI KeyText;
    Tween CooldownDisplayTween;
    Tween CooldownTextTween;
    bool isSelected = false;

    public void StartCooldown(float duration) {
        CooldownText.gameObject.SetActive(true);
        CooldownDisplayTween = Tween.Custom(1, 0, duration, UpdateFillAmount);
        CooldownTextTween = Tween.Custom(duration, 0, duration, UpdateDisplayTime).OnComplete(CleanCooldownText);
    }

    private void UpdateDisplayTime(float value) {
        CooldownText.text = value.ToString("0");
    }
    private void UpdateFillAmount(float value) {
        CooldownOverlay.fillAmount = value;
    }

    private void CleanCooldownText() {
        CooldownText.gameObject.SetActive(false);
    }

    public void SetFocus(bool isFocused) {
        isSelected = isFocused;
        Border.gameObject.SetActive(isSelected);
    }

    public void SetAbilityNumber(int num) {
        KeyText.text = num.ToString();
    }

    [ContextMenu("Test")]
    public void CD() {
        StartCooldown(5);
    }
}
