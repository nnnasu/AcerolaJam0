using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KaimiraGames;
using UnityEngine;

namespace Core.Directors.Common {

    [CreateAssetMenu(fileName = "EnemyWeights", menuName = "EnemyWeights", order = 0)]
    public class EnemyWeights : ScriptableObject {

        public List<EnemySpawnParameters> weights;
        public WeightedList<EnemySpawnParameters> WeightedEnemyList;

        private void OnEnable() {
            List<WeightedListItem<EnemySpawnParameters>> items = weights
                .Select(x => new WeightedListItem<EnemySpawnParameters>(x, x.weight))
                .ToList();
            WeightedEnemyList = new(items);
        }

        public EnemySpawnParameters GetRandomSpawn() {
            return WeightedEnemyList.Next();
        }

    }
}