using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities.Definitions;
using Core.Abilities.Instances;
using UnityEngine;

namespace Core.UI.Rewards {
    public class RewardPanel : MonoBehaviour {
        public List<AbilitySlotSelection> ActionSlots = new();
        public List<AbilitySlotSelection> ModifierSlots = new();

        [HideInInspector]
        public List<ActionInstance> actionInstances = new();

        [HideInInspector]
        public List<ModifierInstance> modifierInstances = new();

        public ActionDefinition DefaultAction;
        public ModifierDefinition DefaultModifier;

        private void Awake() {
            for (int i = 0; i < 4; i++) {
                actionInstances.Add(new(DefaultAction));
                modifierInstances.Add(new(DefaultModifier));
            }
        }


        public event Action<int, bool> OnSelectedEvent = delegate { };
        public event Action<int, bool, Vector2> OnHoverEvent = delegate { };
        public event Action OnHoverLeftEvent = delegate { };

        private void OnHover(int index, bool isModifier, Vector2 pos) {
            OnHoverEvent?.Invoke(index, isModifier, pos);
        }

        private void OnSelected(int index, bool isModifier) {
            OnSelectedEvent?.Invoke(index, isModifier);
        }

        private void OnHoverLeft() {
            OnHoverLeftEvent?.Invoke();
        }

        private void OnEnable() {
            foreach (var item in ActionSlots) {
                item.OnClickEvent += OnSelected;
                item.OnHoverEvent += OnHover;
                item.OnHoverLeft += OnHoverLeft;
            }

            foreach (var item in ModifierSlots) {
                item.OnClickEvent += OnSelected;
                item.OnHoverEvent += OnHover;
                item.OnHoverLeft += OnHoverLeft;
            }
        }
        private void OnDisable() {
            ActionSlots.ForEach(x => x.OnClickEvent -= OnSelected);
            ActionSlots.ForEach(x => x.OnHoverLeft -= OnHoverLeft);
            ActionSlots.ForEach(x => x.OnHoverEvent -= OnHover);
            ModifierSlots.ForEach(x => x.OnClickEvent -= OnSelected);
            ModifierSlots.ForEach(x => x.OnHoverLeft -= OnHoverLeft);
            ModifierSlots.ForEach(x => x.OnHoverEvent -= OnHover);
        }

        public void SetRewards(List<ActionInstance> actions, List<ModifierInstance> modifiers) {
            actionInstances.Clear();
            modifierInstances.Clear();
            actionInstances.AddRange(actions);
            modifierInstances.AddRange(modifiers);

            for (int i = 0; i < ActionSlots.Count; i++) {
                if (i < actionInstances.Count) {
                    ActionSlots[i].SetIcon(actionInstances[i].definition.icon);
                } else {
                    ActionSlots[i].SetIcon();
                }
            }
            for (int i = 0; i < ModifierSlots.Count; i++) {
                if (i < modifierInstances.Count) {
                    ModifierSlots[i].SetIcon(modifierInstances[i].definition.icon);
                } else {
                    ModifierSlots[i].SetIcon();
                }
            }
        }




    }
}