using System.Collections;
using System.Collections.Generic;
using Core.Enemies.Components;
using UnityEngine;

namespace Core.Enemies.Conditions {

    [CreateAssetMenu(fileName = "ConditionBase", menuName = "AI/Conditions/Explosion Charged", order = 0)]
    public class ExplosionChargedCondition : ScriptableObject {

        public bool CheckIfCharged = true;
        public bool CanExecute(AIController controller) {
            if (controller.GetComponent<ExplosionComponent>() is ExplosionComponent ex) {
                if (CheckIfCharged) return ex.ChargeStatus == ChargeStatus.Charged;
                else  return ex.ChargeStatus != ChargeStatus.Charged;
            }
            return false;
        }
    }
}