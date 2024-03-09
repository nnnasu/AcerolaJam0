using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities.Definitions;
using Core.Abilities.Instances;
using Core.Abilities.Structures;
using PrimeTween;
using UnityEngine;

namespace Core.Abilities {
    public partial class AbilityManager : MonoBehaviour {

        public void RecalculateAlignmentLevels() {
            Attributes.levels.Clear();
            BasicAttack.actions.ForEach(y => {
                if (!Attributes.levels.ContainsKey(y.definition.alignment)) Attributes.levels.Add(y.definition.alignment, 0);
                Attributes.levels[y.definition.alignment] += y.level;
            });
            BasicAttack.modifiers.ForEach(y => {
                if (!Attributes.levels.ContainsKey(y.definition.alignment)) Attributes.levels.Add(y.definition.alignment, 0);
                Attributes.levels[y.definition.alignment] += y.level;
            });
            Abilities.ForEach(x => {
                x.actions.ForEach(y => {
                    if (!Attributes.levels.ContainsKey(y.definition.alignment)) Attributes.levels.Add(y.definition.alignment, 0);
                    Attributes.levels[y.definition.alignment] += y.level;
                });
                x.modifiers.ForEach(y => {
                    if (!Attributes.levels.ContainsKey(y.definition.alignment)) Attributes.levels.Add(y.definition.alignment, 0);
                    Attributes.levels[y.definition.alignment] += y.level;
                });
            });
            
            Attributes.UpdateAlignmentStatus();
        }

        public void RecalculateStats() {
            // Apply stat modifiers.
            float oldMaxHP = Attributes.MaxHP;
            float oldMaxMP = Attributes.MaxMP;
            Attributes.ResetState();
            BasicAttack.modifiers.ForEach(x => ApplyGlobalModifiers(x));
            Abilities.ForEach(x => x.modifiers.ForEach(x => ApplyGlobalModifiers(x)));

            Attributes.HP += Mathf.Clamp(Attributes.MaxHP - oldMaxHP, 0, float.MaxValue);
            Attributes.MP += Mathf.Clamp(Attributes.MaxMP - oldMaxMP, 0, float.MaxValue);

            RecalculateAlignmentLevels(); // Add any alignment effects before updating abilities.


            BasicAttack.OnAbilityModified();
            Abilities.ForEach(x => x.OnAbilityModified());

            OnRebindRequest?.Invoke();
        }

        private void ApplyGlobalModifiers(ModifierInstance modifier) {
            modifier.definition.GlobalStatModifier.ForEach(x => {
                Attributes.ApplyModifier(x, modifier.level);
            });
        }

    }
}