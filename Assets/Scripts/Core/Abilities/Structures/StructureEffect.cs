using System.Collections;
using System.Collections.Generic;
using Core.Utilities.Scaling;
using UnityEngine;


namespace Core.Abilities.Structures {
    // [CreateAssetMenu(fileName = "StructureEffect", menuName = "StructureEffect", order = 0)]
    public abstract class StructureEffect : ScriptableObject {

        [Header("Scaling")]
        public ScaledFloat Radius;
        public ScaledFloat DeathDamageMultiplier;

        [Header("Tick Behaviour")]
        public LayerMask LayersToCastFor;

        public abstract void OnTick(StructureBase structure);

        /// <summary>
        /// Perform any other effects when recalled by the player.
        /// Note that Cleanup of the structure is already handled outside of this function.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="structure"></param>
        public abstract void OnRecall(AbilityManager owner, StructureBase structure);

        public virtual void OnDeath(AbilityManager owner, StructureBase structure) {
            owner.Attributes.TakeDamage(
                structure.attributes.MaxHP
                * DeathDamageMultiplier.GetValueAtLevel(structure.level)
                * (1 + owner.Attributes.StructureReboundBonus / 100)
            );
        }


    }
}