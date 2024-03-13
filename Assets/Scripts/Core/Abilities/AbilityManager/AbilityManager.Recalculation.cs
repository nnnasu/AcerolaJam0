using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Abilities.Definitions;
using Core.Abilities.Instances;
using Core.Abilities.Structures;
using PrimeTween;
using UnityEngine;

namespace Core.Abilities {
    public partial class AbilityManager : MonoBehaviour {

        public void RecalculateAlignmentLevels() {
            Attributes.levels.Clear();
            BasicAttack.actions.ForEach(y => Attributes.AddAlignmentLevels(y.definition.alignment, y.definition.GetAlignmentLevel(y.level).Item2));
            BasicAttack.modifiers.ForEach(y => Attributes.AddAlignmentLevels(y.definition.alignment, y.definition.GetAlignmentLevel(y.level).Item2));
            Abilities.ForEach(x => {
                x.actions.ForEach(y => Attributes.AddAlignmentLevels(y.definition.alignment, y.definition.GetAlignmentLevel(y.level).Item2));
                x.modifiers.ForEach(y => Attributes.AddAlignmentLevels(y.definition.alignment, y.definition.GetAlignmentLevel(y.level).Item2));
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
            RecalculateStructureStorage();


            BasicAttack.OnAbilityModified();
            Abilities.ForEach(x => x.OnAbilityModified());

            OnRebindRequest?.Invoke();
        }

        public void RecalculateStructureStorage() {
            // Reset storage states
            foreach (var store in StructureStorage) {
                var storage = store.Value;
                List<StructureBase> toRemove = storage.ActiveStructures.ToList();
                foreach (var item in toRemove) {
                    OnStructureRecall(item, false);
                }
                storage.TotalLevel = 0;
                storage.numberOfAbilities = 0;
                storage.AverageLevel = 0;
                storage.MaxCount = 0;
                storage.CurrentCharges = 0;
            }

            foreach (var item in Abilities) {
                foreach (var action in item.actions) {
                    StructureActionDefinition structureAction = action.definition as StructureActionDefinition;
                    if (!structureAction) continue;
                    var structure = structureAction.structure;
                    if (!StructureStorage.ContainsKey(structure)) StructureStorage.Add(structure, new(this, structure));
                    var store = StructureStorage[structure];
                    store.TotalLevel += action.level;
                    store.numberOfAbilities++;
                    store.AverageLevel = StructureStorage[structure].TotalLevel / StructureStorage[structure].numberOfAbilities;
                    store.MaxCount = structure.GetMaxCount(store.AverageLevel);
                    store.CurrentCharges = store.MaxCount;

                }

            }
        }

        private void ApplyGlobalModifiers(ModifierInstance modifier) {
            modifier.definition.GlobalStatModifier.ForEach(x => {
                Attributes.ApplyModifier(x, modifier.level);
            });
        }

    }
}