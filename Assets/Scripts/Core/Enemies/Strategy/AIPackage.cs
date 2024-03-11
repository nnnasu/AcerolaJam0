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

        public List<AIActionBase> Actions = new();


        public float ExecuteNextAction(AIController controller, Vector3? playerLocation) {
            if (!playerLocation.HasValue) return 0;

            Vector3 location = playerLocation.Value;
            location.y = controller.transform.position.y;
            float distance = Vector3.Distance(location, controller.transform.position);
            foreach (var item in Actions) {
                if (item.Conditions.Count == 0 || item.Conditions.All(x => x.CanExecute(controller))) {
                    controller.CurrentAction = item;
                    return item.Execute(controller, this, playerLocation);
                }
            }

            return 0;
        }

    }
}