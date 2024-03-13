using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using Core.Abilities.Instances;
using UnityEngine;

namespace Core.UI.Targeting {
    [Serializable]
    public struct CursorPreset {
        public Texture2D tex;
        public Vector2 offset;
    }


    public class CursorManager : MonoBehaviour {
        public AbilityManager abilityManager;


        public CursorPreset MenuCursor;
        public CursorPreset CombatCursor;
        public CursorPreset StructureCursor;
        public CursorPreset TargetingCursor;


        public bool isInCombat = true;
        public bool isHoveringOverStructure = false;
        public bool isCurrentlyTargeting = false;

        private void Start() {
            Cursor.lockState = CursorLockMode.Confined;
            SetCursor(CombatCursor);
        }

        private void Update() {
            if (!isInCombat) SetCursor(MenuCursor);
            else {
                if (isCurrentlyTargeting) {
                    SetCursor(TargetingCursor); // Set the current targeting cursor
                } else if (isHoveringOverStructure) SetCursor(StructureCursor);
                else SetCursor(CombatCursor);
            }
        }

        private void OnEnable() {
            abilityManager.OnCurrentSelectedAbilityChanged += SetAbility;
        }

        private void OnDisable() {
            abilityManager.OnCurrentSelectedAbilityChanged -= SetAbility;
        }

        private void SetAbility(AbilityInstance ability) {


        }



        private void SetCursor(CursorPreset preset) {
            Cursor.SetCursor(preset.tex, preset.offset, CursorMode.Auto);
        }


    }
}