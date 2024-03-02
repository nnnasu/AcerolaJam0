using System;
using System.Collections.Generic;
using Core.Abilities.Definitions;
using Core.Abilities.Instances;
using UnityEngine;

namespace Core.Abilities.Templates {

    /// <summary>
    /// Class used to set up default actions for debugging purposes.
    /// </summary>
    [CreateAssetMenu(fileName = "AbilityTemplate", menuName = "Templates/Ability", order = 0)]
    public class AbilityTemplate : ScriptableObject {

        [Serializable]
        public class ActionLevel {
            public ActionDefinition action;
            public int level = 1;
        }

        [Serializable]
        public class ModifierLevel {
            public ModifierDefinition mod;
            public int level = 1;
        }

        public List<ActionLevel> actions = new();
        public List<ModifierLevel> modifiers = new();

    }
}