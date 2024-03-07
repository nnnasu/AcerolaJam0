using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Enemies.Conditions {

    // [CreateAssetMenu(fileName = "ConditionBase", menuName = "ConditionBase", order = 0)]
    public abstract class ConditionBase : ScriptableObject {

        public abstract bool CanExecute(AIController controller);
    }
}