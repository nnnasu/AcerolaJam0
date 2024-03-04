using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Abilities.Instances;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillDisplay : MonoBehaviour {

    private AbilityInstance boundAbility;
    public Image Border;
    public Image MainIcon;
    public Image CooldownOverlay;
    public TextMeshProUGUI CooldownText;
    public TextMeshProUGUI KeyText;
    Tween CooldownDisplayTween;
    Tween CooldownTextTween;
    bool isSelected = false;

    public void OnAbilityStarted() {
    }

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

    public void Bind(AbilityInstance ability) {
        if (ability == null) {
            if (boundAbility != null) Unbind();
            return;
        }

        boundAbility = ability;
        boundAbility.OnFocusChanged += SetFocus;
        boundAbility.OnAbilityActivated += OnAbilityStarted;
        boundAbility.OnCooldownStarted += StartCooldown;
        boundAbility.OnCooldownEnded += CleanCooldownText;
        
        if (boundAbility.actions.Count > 0) {            
            gameObject.SetActive(true);
            MainIcon.sprite = boundAbility.actions[0].definition.icon;
        } else {
            gameObject.SetActive(false);
        }
    }

    public void Unbind() {
        boundAbility.OnFocusChanged -= SetFocus;
        boundAbility.OnAbilityActivated -= OnAbilityStarted;
        boundAbility.OnCooldownStarted -= StartCooldown;
        boundAbility.OnCooldownEnded -= CleanCooldownText;
        boundAbility = null;
        gameObject.SetActive(false);
    }
}
