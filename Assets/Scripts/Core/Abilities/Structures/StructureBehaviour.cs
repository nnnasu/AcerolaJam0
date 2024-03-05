using System.Collections;
using System.Collections.Generic;
using Core.AttributeSystem;
using UnityEngine;
using UnityEngine.Pool;

namespace Core.Abilities.Structures {
    /// <summary>
    /// Component that implements the behaviour. Set on a separate object so that it can refer to another collider more easily.
    /// </summary>
    public abstract class StructureBehaviour : MonoBehaviour {
        public EntityType IgnoredEntities = EntityType.NONE;

    }
}