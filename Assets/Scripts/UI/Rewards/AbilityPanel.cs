using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Abilities.Instances;
using Core.UI.Rewards;
using UnityEngine;

public class AbilityPanel : MonoBehaviour {

    public List<RewardIconSelection> Actions = new();
    public List<RewardIconSelection> Modifiers = new();

    public event Action<int, int, bool> OnSelectedEvent = delegate { };
    public event Action<int, int, bool, Vector2> OnHoverEvent = delegate { };
    public event Action OnHoverLeftEvent = delegate { };

    public int abilityIndex;

    private void OnHover(int index, bool isModifier, Vector2 pos) {
        Debug.Log(pos);
        OnHoverEvent?.Invoke(abilityIndex, index, isModifier, pos);
    }

    private void OnSelected(int index, bool isModifier) {
        Debug.Log("Selected");
        OnSelectedEvent?.Invoke(abilityIndex, index, isModifier);
    }

    private void OnHoverLeft() {
        Debug.Log("Hover Left");
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
        for (int i = 0; i < 4; i++) {
            if (i < ability.actions.Count) {
                var action = ability.actions[i];
                Actions[i].SetIcon(action.definition.icon);
            } else {
                Actions[i].gameObject.SetActive(false);
                Actions[i].SetIcon();
            }
        }
        for (int i = 0; i < 4; i++) {
            if (i < ability.modifiers.Count) {
                var modifier = ability.modifiers[i];
                Modifiers[i].SetIcon(modifier.definition.icon);
            } else {
                Modifiers[i].gameObject.SetActive(false);
                Modifiers[i].SetIcon();
            }
        }
    }

}
