using System;
using System.Collections;
using System.Collections.Generic;
using Core.Enemies.Conditions;
using Core.Enemies.Subactions;
using UnityEngine;

namespace Core.Enemies.Actions {

    // [CreateAssetMenu(fileName = "EnemyAction", menuName = "EnemyAction", order = 0)]
    public abstract class EnemyAction : ScriptableObject {

        public List<ConditionBase> conditions;
        public List<Subaction> Subactions;

        public bool AllowRotations = false;

        public void StartExecution(AIController controller, Action OnComplete) {
            Execute(controller, OnComplete, 0);
        }

        internal async void Execute(AIController controller, Action OnComplete, int index) {
            if (index >= Subactions.Count || !controller.enabled) {
                OnComplete?.Invoke();
                return;
            }

            bool shouldContinue = await Subactions[index].Execute(controller);
            if (!shouldContinue) {
                OnComplete?.Invoke();
                return;
            }
        }

    }
}