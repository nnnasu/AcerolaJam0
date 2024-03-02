using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.AbilitySystem.Alignments;
using UnityEngine;


namespace Core.AbilitySystem.Abilities {
    /// <summary>
    /// Object which is used to populate the default loot tables with "sensible" combinations. 
    /// </summary>
    [CreateAssetMenu(fileName = "AbilityTemplate", menuName = "Abilities/AbilityTemplate", order = 0)]
    public class AbilityTemplate : ScriptableObject {

        public List<ActionTemplateBase> ActionTemplates;
        // Also have modifier templates?

        

    }
}