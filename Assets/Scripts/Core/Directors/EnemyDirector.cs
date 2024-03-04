using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Directors {
    public class EnemyDirector : MonoBehaviour {
        private HashSet<GameObject> enemies = new();
        public event Action OnEnemiesCleared = delegate {};

        public void SpawnEnemies(float credits, int currentGameLevel) {
            
        }
}
}