
using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using Core.Abilities.Structures;
using Core.AttributeSystem;
using UnityEngine;
using UnityEngine.Pool;

namespace Core.AbilityExtensions.Spawns {
    public class VacuumStructureBehaviour : StructureBase {

        public SphereCollider aoe;
        public float force = 10;
        public GameplayEffect EffectToApply;
        public int level;

        private void OnTriggerEnter(Collider other) {
            var target = other.GetComponent<AttributeSet>();
            if (!target) return;

            if ((IgnoredEntities & target.entityType) != 0) {
                // No overlap between flag objects.
                return;
            }
            Rigidbody rb = target.GetComponent<Rigidbody>();
            Vector3 direction = transform.position - target.transform.position;
            direction = direction.normalized * force;
            target.ApplyEffect(new(EffectToApply, level));

            rb.AddForce(direction, ForceMode.Impulse);
        }


    }
}