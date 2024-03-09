using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Enemies.Strategy {

    [Serializable]
    public struct RangeAction {
        public float range;
        public AIActionBase action;

    }


    [CreateAssetMenu(fileName = "AIPackage", menuName = "Enemy AI/Strategy", order = 0)]
    public class AIPackage : ScriptableObject {

        public List<RangeAction> Actions = new();

        [Tooltip("Action that will be used if nothing else is valid.")]
        public AIActionBase FallbackAction;

        public float ExecuteNextAction(AIController controller, Vector3? playerLocation) {
            if (!playerLocation.HasValue) return 0;

            Vector3 location = playerLocation.Value;
            location.y = controller.transform.position.y;
            float distance = Vector3.Distance(location, controller.transform.position);
            foreach (var item in Actions) {
                if (distance <= item.range) {
                    controller.CurrentAction = item.action;
                    return item.action.Execute(controller, this, playerLocation);
                }
            }

            if (!FallbackAction) return 0;
            controller.CurrentAction = FallbackAction;
            return FallbackAction.Execute(controller, this, playerLocation);
        }

    }
}