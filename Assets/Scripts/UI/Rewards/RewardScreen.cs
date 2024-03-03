using System.Collections.Generic;
using Core.Abilities;
using UnityEngine;

namespace Core.UI.Rewards {
    public class RewardScreen : MonoBehaviour {

        public List<AbilityPanel> panels = new();
        public HoverTipManager hoverTipManager;
        private AbilityManager abilityManager;

        private void OnEnable() {
            panels.ForEach(x => {
                x.OnHoverEvent += OnHover;
                x.OnHoverLeftEvent += OnHoverLeft;
                x.OnSelectedEvent += OnSelected;
            });
        }
        

        private void OnHover(int abilityIndex, int iconIndex, bool isModifier, Vector2 pos) {
            Debug.Log("hover");
            hoverTipManager.ShowTip("xdxdxdxdxd", pos);
        }

        private void OnSelected(int abilityIndex, int iconIndex, bool isModifier) {

        }
        private void OnHoverLeft() {
            hoverTipManager.HideTip();

        }

        public void LoadPlayer(AbilityManager manager) {
            this.abilityManager = manager;
            for (int i = 0; i < panels.Count; i++) {
                if (i >= manager.Abilities.Count) {
                    // Set to empty?
                    panels[i].gameObject.SetActive(false);
                } else {
                    panels[i].gameObject.SetActive(true);
                    panels[i].SetAbility(manager.Abilities[i]);
                }
            }
        }

    }
}