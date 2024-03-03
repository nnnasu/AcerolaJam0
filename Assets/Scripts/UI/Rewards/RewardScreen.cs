using System.Collections.Generic;
using Core.Abilities;
using UnityEngine;

namespace Core.UI.Rewards {
    public class RewardScreen : MonoBehaviour {

        public List<AbilityPanel> panels = new();

        public void LoadPlayer(AbilityManager manager) {
            for (int i = 0; i < panels.Count; i++) {
                if (i >= manager.Abilities.Count) {
                    // Set to empty?
                    panels[i].gameObject.SetActive(false);
                } else {
                    panels[i].SetAbility(manager.Abilities[i]);
                }
            }
        }

    }
}