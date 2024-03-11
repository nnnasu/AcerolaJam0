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
    public StatusEffectDisplay statusEffectDisplay;
    public List<SkillDisplay> Skills = new();

    [Header("Player Reference")]
    public AbilityManager abilityManager;
    public PlayerAttributeSet attributes => abilityManager.Attributes;
    public HoverTipManager HoverManager;



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
        for (int i = 0; i < Skills.Count; i++) {
            if (i >= abilityCount) {
                Skills[i].Bind(null);
            } else {
                Skills[i].Bind(abilityManager.Abilities[i]);
            }
        }
    }


    private void OnEnable() {
        attributes.OnHPChanged += UpdateHP;
        attributes.OnMPChanged += UpdateMP;
        abilityManager.OnRebindRequest += Synchronise;
        attributes.OnEffectApplied += statusEffectDisplay.ApplyEffect;
        attributes.OnEffectRemoved += statusEffectDisplay.RemoveEffect;
        foreach (var item in Skills) {
            item.OnHoverEvent += OnHoverEvent;
            item.OnHoverLeft += OnHoverLeft;
        }
    }

    private void OnDisable() {
        attributes.OnHPChanged -= UpdateHP;
        attributes.OnMPChanged -= UpdateMP;
        abilityManager.OnRebindRequest -= Synchronise;
        attributes.OnEffectApplied -= statusEffectDisplay.ApplyEffect;
        attributes.OnEffectRemoved -= statusEffectDisplay.RemoveEffect;
        foreach (var item in Skills) {
            item.OnHoverEvent -= OnHoverEvent;
            item.OnHoverLeft -= OnHoverLeft;
        }
    }

    private void UpdateMP(float oldValue, float newValue) {
        MPBar.OnValueChanged(oldValue, newValue, attributes.MaxMP);
    }

    private void UpdateHP(float oldValue, float newValue) {
        HPBar.OnValueChanged(oldValue, newValue, attributes.MaxHP);
    }

    private void OnHoverEvent(int index, Vector2 pos) {
        var icon = Skills[index - 1];
        if (icon.boundAbility == null) return;

        var ability = icon.boundAbility;
        HoverManager.ShowTip("", ability.GetDescription(), pos);

    }
    private void OnHoverLeft() {
        HoverManager.HideTip();

    }

}
