using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KaimiraGames;
using UnityEngine;

namespace Core.Directors.Common {

    [CreateAssetMenu(fileName = "EnemyWeights", menuName = "Rooms/EnemyWeights", order = 0)]
    public class EnemyWeights : ScriptableObject {

        public List<EnemySpawnParameters> weights;
        public WeightedList<EnemySpawnParameters> WeightedEnemyList;

        public List<EnemyModifierParameters> modifierWeights;
        public WeightedList<EnemyModifierParameters> WeightedModifierList;

        private void OnEnable() {
            List<WeightedListItem<EnemySpawnParameters>> items = weights
                .Select(x => new WeightedListItem<EnemySpawnParameters>(x, x.weight))
                .ToList();
            WeightedEnemyList = new(items);

            List<WeightedListItem<EnemyModifierParameters>> mods = modifierWeights
                .Select(x => new WeightedListItem<EnemyModifierParameters>(x, x.weight))
                .ToList();
            WeightedModifierList = new(mods);
        }

        public EnemySpawnParameters GetRandomSpawn() {
            return WeightedEnemyList.Next();
        }

        public EnemyModifierParameters GetRandomMod() {
            return WeightedModifierList.Next();
        }

    }
}