
using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using Core.Abilities.Structures;
using Core.AttributeSystem;
using Core.Utilities.Scaling;
using UnityEngine;
using UnityEngine.Pool;

namespace Core.AbilityExtensions.Spawns {
    /// <summary>
    /// Class that heals all valid targets within range upon each tick. 
    /// 
    /// </summary>

    [CreateAssetMenu(fileName = "Heal Structure Effect", menuName = "Ability System/Player Actions/Spawns/Effects/Heal", order = 0)]
    public class HealStructure : StructureEffect {

        public ScaledFloat HealAmount;
        public ScaledFloat RecallHealAmount;


        public override void OnRecall(AbilityManager owner, StructureBase structure) {

            int count = 0;
            var colliders = structure.OverlapAoe(out count);

            float healAmount = RecallHealAmount.GetValueAtLevel(structure.level) * (1 + Formulas.StructureEnmityBonus(structure.GetPercentageHP()));
            for (int i = 0; i < count; i++) {
                var target = colliders[i].GetComponent<AttributeSet>();
                if (!target) continue;
                if ((structure.IgnoredEntities & target.GetEntityType()) != 0) continue; // Entity is inside ignored types

                target.Heal(healAmount);
            }
        }

        public override void OnTick(StructureBase structure) {
            int count = 0;
            var colliders = structure.OverlapAoe(out count);
            float healAmount = HealAmount.GetValueAtLevel(structure.level) * (1 + Formulas.StructureEnmityBonus(structure.GetPercentageHP()));

            for (int i = 0; i < count; i++) {
                var target = colliders[i].GetComponent<IDamageable>();
                if (target == null) continue;
                if ((structure.IgnoredEntities & target.GetEntityType()) != 0) continue; // Entity is inside ignored types

                target.Heal(healAmount);
            }
        }

    }
}