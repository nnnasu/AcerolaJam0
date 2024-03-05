using System.Collections;
using System.Collections.Generic;
using Core.AttributeSystem;
using UnityEngine;
using UnityEngine.Pool;

namespace Core.Abilities.Structures {
    public class StructureBase : PoolableBehaviour {
        public AttributeSet attributes;
        public EntityType IgnoredEntities = EntityType.NONE;

        public virtual void OnSpawn() {
            attributes.ResetState();
        }

        public virtual void OnRecall(AbilityManager manager) {
            Debug.Log("Recalled");

        }

    }
}