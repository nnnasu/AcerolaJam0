using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.AbilitySystem.Abilities {
    public enum TargetingType {

        [Tooltip("Draws a circle around the player.")]
        NoTarget,

        [Tooltip("Draws a straight line from current position to target.")]
        DirectionalArea,

        [Tooltip("Draws a circle around the cursor position.")]
        PointTarget,

        [Tooltip("Draws an arrow in the direction the cursor is facing.")]
        Direction
    }
}