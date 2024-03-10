using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Enemies.Conditions {

    [CreateAssetMenu(fileName = "ConditionBase", menuName = "AI/Conditions/Player In Range", order = 0)]
    public class RangeCondition : ConditionBase {

        public float range;
        public bool TrueIfWithinRange = true;

        public override bool CanExecute(AIController controller) {
            if (TrueIfWithinRange) {
                return controller.currentPlayerDistance <= range;
            } else return controller.currentPlayerDistance > range;
        }
    }
}