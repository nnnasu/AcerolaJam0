using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Abilities.Instances;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillDisplay : MonoBehaviour, IPointerMoveHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

    internal AbilityInstance boundAbility;
    public Image Border;
    public Image MainIcon;
    public Image CooldownOverlay;
    public TextMeshProUGUI CooldownText;
    public TextMeshProUGUI KeyText;
    public TextMeshProUGUI MPCost;

    Tween CooldownDisplayTween;
    Tween CooldownTextTween;
    bool isSelected = false;
    public int index = 0;


    public event Action<int, Vector2> OnHoverEvent = delegate { };
    public event Action<int> OnClickEvent = delegate { };
    public event Action OnHoverLeft = delegate { };

    public void OnAbilityStarted() {
    }

    public void StartCooldown(float duration) {
        CooldownText.gameObject.SetActive(true);
        if (duration <= float.Epsilon) {
            CleanCooldownText();
            return;
        }
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
        index = num;
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
        boundAbility.OnAbilityChanged += UpdateAbilityDisplay;
        MPCost.text = boundAbility.cachedMPCost.ToString();


        if (boundAbility.actions.Count > 0) {
            gameObject.SetActive(true);
            MainIcon.sprite = boundAbility.actions[0].definition.icon;
        } else {
            gameObject.SetActive(false);
        }
    }

    private void UpdateAbilityDisplay() {
        MPCost.text = boundAbility.cachedMPCost.ToString();

    }

    public void Unbind() {
        boundAbility.OnFocusChanged -= SetFocus;
        boundAbility.OnAbilityActivated -= OnAbilityStarted;
        boundAbility.OnCooldownStarted -= StartCooldown;
        boundAbility.OnCooldownEnded -= CleanCooldownText;
        boundAbility.OnAbilityChanged -= UpdateAbilityDisplay;
        boundAbility = null;
        gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        OnHoverEvent?.Invoke(index, eventData.position);
    }

    public void OnPointerExit(PointerEventData eventData) {
        OnHoverLeft?.Invoke();
    }

    public void OnPointerMove(PointerEventData eventData) {
        OnHoverEvent?.Invoke(index, eventData.position);
    }

    public void OnPointerClick(PointerEventData eventData) {

    }
}
