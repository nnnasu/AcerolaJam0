using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using Core.Abilities.Instances;
using UnityEngine;
using UnityEngine.Rendering.Universal;


namespace Core.Targeting {
    public class AimManager : MonoBehaviour {
        public AbilityManager abilityManager;
        public List<ProjectionManager> projectors = new();
        public PlayerController player;

        private void OnEnable() {
            abilityManager.OnCurrentSelectedAbilityChanged += OnAbilityChanged;
        }
        private void OnDisable() {
            abilityManager.OnCurrentSelectedAbilityChanged -= OnAbilityChanged;
        }

        private void OnAbilityChanged(AbilityInstance ability) {
            for (int i = 0; i < projectors.Count; i++) {
                if (i >= ability.actions.Count) {
                    projectors[i].SetAction();
                } else {
                    projectors[i].SetAction(ability.actions[i]);
                }
            }
        }

        private void Update() {
            Vector3 mousePos = player.mousePosition;
            projectors.ForEach(x => x.Tick(mousePos));            
        }


    }
}