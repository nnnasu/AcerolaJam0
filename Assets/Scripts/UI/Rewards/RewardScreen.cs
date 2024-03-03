using System.Collections.Generic;
using Core.Abilities;
using Core.Abilities.Instances;
using UnityEngine;

namespace Core.UI.Rewards {
    internal class Selection {
        public int SelectedAbility;
        public int SelectedSlot;
        public bool isModifierSelected;
    }

    public class RewardScreen : MonoBehaviour {

        public AbilityPanel BasicAttackPanel;
        public List<AbilityPanel> panels = new();
        public HoverTipManager hoverTipManager;
        private AbilityManager abilityManager;
        private Selection inventorySelection;
        private Selection rewardSelection;
        public AbilityPanel RewardPanel;
        public AbilityInstance RewardAbility = new(null);


        private void OnEnable() {
            panels.ForEach(x => {
                x.OnHoverEvent += OnHover;
                x.OnHoverLeftEvent += OnHoverLeft;
                x.OnSelectedEvent += OnSelected;
            });
            BasicAttackPanel.OnHoverEvent += OnHover;
            BasicAttackPanel.OnHoverLeftEvent += OnHoverLeft;
            BasicAttackPanel.OnSelectedEvent += OnSelected;

            RewardPanel.OnHoverEvent += OnHoverReward;
            RewardPanel.OnHoverLeftEvent += OnHoverLeft;
            RewardPanel.OnSelectedEvent += OnSelectedReward;
        }
        private void OnHover(int abilityIndex, int slotIndex, bool isModifier, Vector2 pos) {
            AbilityInstance abilityInstance = null;
            if (abilityIndex == -1) abilityInstance = abilityManager.BasicAttack;
            else abilityInstance = abilityManager.Abilities[abilityIndex];

            if (isModifier) {
                if (slotIndex >= abilityInstance.modifiers.Count) {
                    hoverTipManager.ShowTip("Empty Modifier", pos);
                    return;
                }

                var slot = abilityInstance.modifiers[slotIndex];
                hoverTipManager.ShowTip(slot.ToTooltipText(), pos);
            } else {
                if (slotIndex >= abilityInstance.actions.Count) {
                    hoverTipManager.ShowTip("Empty Action", pos);
                    return;
                }
                var slot = abilityInstance.actions[slotIndex];
                hoverTipManager.ShowTip(slot.ToTooltipText(), pos);
            }
        }

        private void OnHoverReward(int abilityIndex, int iconIndex, bool isModifier, Vector2 pos) {
            // TODO: Create reward ability so that this can be used. 
            
            hoverTipManager.ShowTip("xdxdxdxdxd", pos);
        }

        private void OnSelected(int abilityIndex, int iconIndex, bool isModifier) {
            if (inventorySelection != null) {
                // Clear the previous 
                var prev = GetIcon(inventorySelection);
                prev.SetSelected(false);

                inventorySelection.SelectedAbility = abilityIndex;
                inventorySelection.SelectedSlot = iconIndex;
                inventorySelection.isModifierSelected = isModifier;
                var current = GetIcon(inventorySelection);
                current.SetSelected(true);
            } else {
                inventorySelection = new() {
                    SelectedAbility = abilityIndex,
                    SelectedSlot = iconIndex,
                    isModifierSelected = isModifier
                };
                GetIcon(inventorySelection).SetSelected(true);
            }
        }

        private void OnSelectedReward(int abilityIndex, int iconIndex, bool isModifier) {
            if (rewardSelection != null) {
                var prev = GetIconFromReward(rewardSelection);
                prev.SetSelected(false);

                rewardSelection.SelectedAbility = abilityIndex;
                rewardSelection.SelectedSlot = iconIndex;
                rewardSelection.isModifierSelected = isModifier;
                var current = GetIconFromReward(rewardSelection);
                current.SetSelected(true);
            } else {
                rewardSelection = new() {
                    SelectedAbility = abilityIndex,
                    SelectedSlot = iconIndex,
                    isModifierSelected = isModifier
                };
                var current = GetIconFromReward(rewardSelection);
                current.SetSelected(true);
            }
        }

        private AbilitySlotSelection GetIcon(Selection selection) {
            AbilityPanel panel = selection.SelectedAbility == -1 ? BasicAttackPanel : panels[selection.SelectedAbility];
            if (selection.isModifierSelected) return panel.Modifiers[selection.SelectedSlot];
            else return panel.Actions[selection.SelectedSlot];
        }

        private AbilitySlotSelection GetIconFromReward(Selection selection) {
            if (selection.isModifierSelected) return RewardPanel.Modifiers[selection.SelectedSlot];
            else return RewardPanel.Actions[selection.SelectedSlot];
        }

        private void OnHoverLeft() {
            hoverTipManager.HideTip();

        }

        public void LoadPlayer(AbilityManager manager) {
            this.abilityManager = manager;
            BasicAttackPanel.SetAbility(abilityManager.BasicAttack);
            for (int i = 0; i < panels.Count; i++) {
                if (i >= manager.Abilities.Count) {
                    // Set to empty?
                    // panels[i].gameObject.SetActive(false);
                    // panels[i].SetAbility(null);
                } else {
                    // panels[i].gameObject.SetActive(true);
                    panels[i].SetAbility(manager.Abilities[i]);
                }
            }
        }

    }
}