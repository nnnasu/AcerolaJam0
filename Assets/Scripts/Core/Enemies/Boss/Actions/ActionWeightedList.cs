using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KaimiraGames;
using UnityEngine;

namespace Core.Enemies.Boss.Actions {
    [Serializable]
    public class ActionWeights {
        public BossAction action;
        public int weight;
    }

    [CreateAssetMenu(fileName = "ActionWeightedList", menuName = "Boss/Weighted Action List", order = 0)]
    public class ActionWeightedList : ScriptableObject {

        [SerializeField] internal List<ActionWeights> weights = new();

        public WeightedList<BossAction> actions;

        private void OnEnable() {
            List<WeightedListItem<BossAction>> items = weights
                .Select(x => new WeightedListItem<BossAction>(x.action, x.weight))
                .ToList();
            actions = new(items);
        }

        public BossAction GetAction() {
            return actions.Next();
        }



    }
}