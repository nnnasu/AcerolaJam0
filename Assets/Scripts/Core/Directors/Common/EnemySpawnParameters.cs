using System;
using UnityEngine;


namespace Core.Directors.Common {
    [Serializable]
    public class EnemySpawnParameters {

        public GameObject prefab;
        public int weight;
        public float cost;
    }
}