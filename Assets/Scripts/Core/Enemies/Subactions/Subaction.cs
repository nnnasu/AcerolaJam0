using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.Enemies.Subactions {

    /// <summary>
    /// Reusable bits of actions that can be chained into an Action. 
    /// </summary>
    // [CreateAssetMenu(fileName = "Subaction", menuName = "Subaction", order = 0)]
    public abstract class Subaction : ScriptableObject {

        public async Task<bool> Execute(AIController controller) {
            // Guarantee that the controller is active when starting a sub-action.
            // Note that the controller may be disabled (NPC kill) while this is still running. 
            // The implementation function needs to check that the controller is still enabled before doing anything.

            if (!controller.enabled) return false;
            return await ExecuteImplementation(controller);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public abstract Task<bool> ExecuteImplementation(AIController controller);


    }
}