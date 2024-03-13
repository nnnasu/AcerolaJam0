using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KaimiraGames;
using UnityEngine;

namespace Core.Enemies.Boss.Actions {
    [Serializable]
    public class MovementWeights {
        public MovementStrategy action;
        public int weight;
    }


    [CreateAssetMenu(fileName = "MovementWeightedList", menuName = "Boss/MovementWeightedList", order = 0)]
    public class MovementWeightedList : ScriptableObject {
        [SerializeField] internal List<MovementWeights> weights = new();

        public WeightedList<MovementStrategy> movements;

        private void OnEnable() {
            List<WeightedListItem<MovementStrategy>> items = weights
                .Select(x => new WeightedListItem<MovementStrategy>(x.action, x.weight))
                .ToList();
            movements = new(items);
        }

        public MovementStrategy GetMovement() {
            return movements.Next();
        }

    }
}