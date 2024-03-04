using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Core.Abilities.Structures {
    public class StructureBase : PoolableBehaviour {
        public AttributeSet attributes;

        public virtual void OnSpawn() {
            attributes.ResetState();
        }

        public virtual void OnRecall(AbilityManager manager) {
            Debug.Log("Recalled");

        }

    }
}