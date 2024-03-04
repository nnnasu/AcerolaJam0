using System.Collections.Generic;
using Core.Abilities;
using Core.Abilities.Instances;
using UnityEngine;
using UnityEngine.UI;

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
        public RewardPanel RewardPanel;
        public Button swapButton;
        private bool CanSwap = false;

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
            swapButton.onClick.AddListener(OnButtonClicked);
        }


        private void OnButtonClicked() {
            if (!CanSwap) return;

            AbilityInstance currentAbility;
            if (inventorySelection.SelectedAbility == -1) currentAbility = abilityManager.BasicAttack;
            else currentAbility = abilityManager.Abilities[inventorySelection.SelectedAbility];
            
            if (rewardSelection.isModifierSelected) {
                var reward = RewardPanel.modifierInstances[rewardSelection.SelectedSlot];
                currentAbility.SwapModifier(reward, inventorySelection.SelectedSlot);
            } else {
                var reward = RewardPanel.actionInstances[rewardSelection.SelectedSlot];
                currentAbility.SwapAction(reward, inventorySelection.SelectedSlot);
            }

            abilityManager.RecalculateStats(); // rebind 
            LoadPlayerData(abilityManager);
        }


        public void LoadPlayerData(AbilityManager manager) {
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

        private bool CheckSelectionTypesCompatible() {
            // Match action/modifier slot types
            if (inventorySelection.isModifierSelected != rewardSelection.isModifierSelected) return false;
            if (inventorySelection.SelectedAbility == -1
                && !inventorySelection.isModifierSelected
                && RewardPanel.actionInstances[rewardSelection.SelectedSlot].definition.actionType
                    != Abilities.Enums.ActionType.BasicAttack) {
                // Basic attack action should only be basic attack
                return false;
            }

            return true;
        }



        private void ValidateSelection() {
            if (inventorySelection == null || rewardSelection == null) {
                InvalidateSwap();
                return;
            }

            AbilityInstance currentAbility;
            if (inventorySelection.SelectedAbility == -1) currentAbility = abilityManager.BasicAttack;
            else currentAbility = abilityManager.Abilities[inventorySelection.SelectedAbility];

            if (!CheckSelectionTypesCompatible()) {
                InvalidateSwap();
                return;
            }
            if (inventorySelection.isModifierSelected) {
                var modifier = RewardPanel.modifierInstances[inventorySelection.SelectedSlot];
                var status = currentAbility.CanSwapModifier(modifier, inventorySelection.SelectedSlot);
                if (status == Abilities.Enums.AddStatus.Available) ApproveSwap();
                else InvalidateSwap();
            } else {
                var action = RewardPanel.actionInstances[inventorySelection.SelectedSlot];
                var status = currentAbility.CanSwapAction(action, inventorySelection.SelectedSlot);
                if (status == Abilities.Enums.AddStatus.Available) ApproveSwap();
                else InvalidateSwap();
            }
        }

        private void ApproveSwap() {
            CanSwap = true;
            swapButton.interactable = true;
        }

        private void InvalidateSwap() {
            CanSwap = false;
            swapButton.interactable = false;
        }

        #region selection

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
            ValidateSelection();
        }

        private void OnSelectedReward(int iconIndex, bool isModifier) {
            if (rewardSelection != null) {
                var prev = GetIconFromReward(rewardSelection);
                prev.SetSelected(false);

                rewardSelection.SelectedAbility = 0;
                rewardSelection.SelectedSlot = iconIndex;
                rewardSelection.isModifierSelected = isModifier;
                var current = GetIconFromReward(rewardSelection);
                current.SetSelected(true);
            } else {
                rewardSelection = new() {
                    SelectedAbility = 0,
                    SelectedSlot = iconIndex,
                    isModifierSelected = isModifier
                };
                var current = GetIconFromReward(rewardSelection);
                current.SetSelected(true);
            }
            ValidateSelection();
        }
        private AbilitySlotSelection GetIcon(Selection selection) {
            AbilityPanel panel = selection.SelectedAbility == -1 ? BasicAttackPanel : panels[selection.SelectedAbility];
            if (selection.isModifierSelected) return panel.Modifiers[selection.SelectedSlot];
            else return panel.Actions[selection.SelectedSlot];
        }

        private AbilitySlotSelection GetIconFromReward(Selection selection) {
            if (selection.isModifierSelected) return RewardPanel.ModifierSlots[selection.SelectedSlot];
            else return RewardPanel.ActionSlots[selection.SelectedSlot];
        }

        #endregion

        #region hover
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
        private void OnHoverReward(int iconIndex, bool isModifier, Vector2 pos) {
            // TODO: Create reward ability so that this can be used. 
            if (isModifier) {
                string result = RewardPanel.modifierInstances[iconIndex].ToTooltipText();
                hoverTipManager.ShowTip(result, pos);
            } else {
                string result = RewardPanel.actionInstances[iconIndex].ToTooltipText();
                hoverTipManager.ShowTip(result, pos);
            }
        }
        private void OnHoverLeft() {
            hoverTipManager.HideTip();
        }


        #endregion

    }
}