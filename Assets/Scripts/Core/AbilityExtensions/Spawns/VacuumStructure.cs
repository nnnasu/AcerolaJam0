
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
    /// Class that sucks all valid targets within range upon each tick and slows their movement speed.
    /// </summary>

    [CreateAssetMenu(fileName = "Vacuum Structure Effect", menuName = "Ability System/Player Actions/Spawns/Effects/Vacuum", order = 0)]
    public class VacuumStructure : StructureEffect {

        public ScaledFloat SuckDamage;
        public ScaledFloat RecallDamage;
        public float Force;


        public override void OnRecall(AbilityManager owner, StructureBase structure) {
            int count;
            var colliders = structure.OverlapAoe(out count);

            float damage = RecallDamage.GetValueAtLevel(structure.level) * (1 + Formulas.StructureEnmityBonus(structure.GetPercentageHP()));
            for (int i = 0; i < count; i++) {
                var damageable = colliders[i].GetComponent<IDamageable>();
                if (damageable == null) continue;
                if ((structure.IgnoredEntities & damageable.GetEntityType()) != 0) continue; // Entity is inside ignored types
                damageable.TakeDamage(damage);

                var target = damageable as AttributeSet;
                if (!target) continue;
                if (target.GetComponent<Rigidbody>() is Rigidbody rb && rb != null) {
                    Vector3 direction = structure.transform.position + structure.Center - rb.transform.position;
                    direction.Normalize();
                    rb.AddForce(direction * Force, ForceMode.Impulse);
                }

            }
        }

        public override void OnTick(StructureBase structure) {
            int count;
            var colliders = structure.OverlapAoe(out count);

            float damage = SuckDamage.GetValueAtLevel(structure.level);
            for (int i = 0; i < count; i++) {
                var damageable = colliders[i].GetComponent<IDamageable>();
                if (damageable == null) continue;
                if ((structure.IgnoredEntities & damageable.GetEntityType()) != 0) continue; // Entity is inside ignored types
                damageable.TakeDamage(damage);

                var target = damageable as AttributeSet;
                if (!target) continue;
                if (target.GetComponent<Rigidbody>() is Rigidbody rb && rb != null) {
                    Vector3 direction = structure.transform.position + structure.Center - rb.transform.position;
                    direction.Normalize();
                    rb.AddForce(direction * Force, ForceMode.Impulse);
                }

            }
        }

    }
}