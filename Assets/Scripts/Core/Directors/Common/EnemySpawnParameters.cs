using System;
using UnityEngine;


namespace Core.Directors.Common {
    [Serializable]
    public class EnemySpawnParameters {

        public GameObject prefab;
        public int weight;
        public float cost;
    }
    [Serializable]
    public class EnemyModifierParameters {
        public StatusEffect Modification;

        [Tooltip("Level of the effect is based on the game level multiplied by this.")]
        public float levelMultiplier;
        public int weight = 1;
        public float cost = 5;
        public int GetLevel(int CurrentGameLevel) => Mathf.FloorToInt(levelMultiplier * CurrentGameLevel);
    }
}