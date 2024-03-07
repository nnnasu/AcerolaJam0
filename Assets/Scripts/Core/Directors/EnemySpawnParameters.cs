using System;
using UnityEngine;


namespace Core.Directors {
    [Serializable]
    public class EnemySpawnParameters {

        public GameObject prefab;
        public int weight;
        public float cost;
    }
}