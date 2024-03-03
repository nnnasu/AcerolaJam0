using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour {
    public HorizontalLayoutGroup AbilityContainer;
    public ValueBar HPBar;
    public ValueBar MPBar;
    public SkillDisplay AbilityPrefab;

    [Header("Player Reference")]
    public AbilityManager abilityManager;
    public PlayerAttributeSet attributes => abilityManager.Attributes;

    private void Start() {
        // Tween.Delay(1, Synchronise);
        Synchronise();
    }

    private void Synchronise() {
        // Setup attributes
        SyncAttributeValues();
        BindAbilities();
    }

    private void SyncAttributeValues() {
        HPBar.OnValueChanged(attributes.HP, attributes.HP, attributes.MaxHP);
        MPBar.OnValueChanged(attributes.MP, attributes.MP, attributes.MaxMP);
    }

    private void BindAbilities() {
        int abilityCount = abilityManager.Abilities.Count;
        for (int i = 0; i < abilityCount; i++) {
            SkillDisplay display = Instantiate(AbilityPrefab, AbilityContainer.transform);
            display.Bind(abilityManager.Abilities[i]);
        }

    }

    private void OnEnable() {
        attributes.OnHPChanged += UpdateHP;
        attributes.OnMPChanged += UpdateMP;
    }

    private void OnDisable() {
        attributes.OnHPChanged -= UpdateHP;
        attributes.OnMPChanged -= UpdateMP;
    }

    private void UpdateMP(float oldValue, float newValue) {
        MPBar.OnValueChanged(oldValue, newValue, attributes.MaxMP);
    }

    private void UpdateHP(float oldValue, float newValue) {
        HPBar.OnValueChanged(oldValue, newValue, attributes.MaxHP);
    }

}
