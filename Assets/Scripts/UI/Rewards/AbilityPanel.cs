using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Abilities.Instances;
using Core.UI.Rewards;
using UnityEngine;

public class AbilityPanel : MonoBehaviour {

    public List<AbilitySlotSelection> Actions = new();
    public List<AbilitySlotSelection> Modifiers = new();

    public event Action<int, int, bool> OnSelectedEvent = delegate { };
    public event Action<int, int, bool, Vector2, bool> OnHoverEvent = delegate { };
    public event Action OnHoverLeftEvent = delegate { };

    public int abilityIndex;

    private void OnHover(int index, bool isModifier, Vector2 pos, bool wasHovered) {
        OnHoverEvent?.Invoke(abilityIndex, index, isModifier, pos, wasHovered);
    }

    private void OnSelected(int index, bool isModifier) {
        OnSelectedEvent?.Invoke(abilityIndex, index, isModifier);
    }

    private void OnHoverLeft() {
        OnHoverLeftEvent?.Invoke();
    }

    private void OnEnable() {
        foreach (var item in Actions) {
            item.OnClickEvent += OnSelected;
            item.OnHoverEvent += OnHover;
            item.OnHoverLeft += OnHoverLeft;
        }

        foreach (var item in Modifiers) {
            item.OnClickEvent += OnSelected;
            item.OnHoverEvent += OnHover;
            item.OnHoverLeft += OnHoverLeft;
        }
    }

    private void OnDisable() {
        Actions.ForEach(x => x.OnClickEvent -= OnSelected);
        Actions.ForEach(x => x.OnHoverLeft -= OnHoverLeft);
        Actions.ForEach(x => x.OnHoverEvent -= OnHover);
        Modifiers.ForEach(x => x.OnClickEvent -= OnSelected);
        Modifiers.ForEach(x => x.OnHoverLeft -= OnHoverLeft);
        Modifiers.ForEach(x => x.OnHoverEvent -= OnHover);
    }

    public void SetAbility(AbilityInstance ability) {
        for (int i = 0; i < Actions.Count; i++) {
            if (i < ability.actions.Count) {
                var action = ability.actions[i];
                Actions[i].SetIcon(action.definition.icon);
            } else {
                // Actions[i].gameObject.SetActive(false);
                Actions[i].SetIcon();
            }
        }
        for (int i = 0; i < 4; i++) {
            if (i < ability.modifiers.Count) {
                var modifier = ability.modifiers[i];
                Modifiers[i].SetIcon(modifier.definition.icon);
            } else {
                // Modifiers[i].gameObject.SetActive(false);
                Modifiers[i].SetIcon();
            }
        }
    }

}
