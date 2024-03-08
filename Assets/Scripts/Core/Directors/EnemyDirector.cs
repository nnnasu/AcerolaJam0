using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Core.GlobalInfo;
using KaimiraGames;
using UnityEngine;
using PrimeTween;

namespace Core.Directors {
    public class EnemyDirector : MonoBehaviour {
        private HashSet<GameObject> enemies = new();
        public event Action OnEnemiesCleared = delegate { };
        public EnemyWeights weights;
        public GameObject CheckpointPrefab;
        public WeightedList<EnemySpawnParameters> WeightedEnemyList;
        public Dictionary<EnemySpawnParameters, int> EnemySpawnCount = new();

        private void Awake() {
            List<WeightedListItem<EnemySpawnParameters>> items = weights.weights
                .Select(x => new WeightedListItem<EnemySpawnParameters>(x, x.weight))
                .ToList();
            WeightedEnemyList = new(items);
        }

        private void Start() {
            // TODO: Fix this
            Tween.Delay(5, SpawnCheckpoint);
        }

        public void SpawnCheckpoint() {
            // TODO: Use this to spawn additional checkpoint types
            var pt = GlobalPool.Current.GetObject(CheckpointPrefab);            
            pt.SetActive(true);
            pt.transform.position = new Vector3(0, 1, 5);
        }

        public void SpawnEnemies(int currentGameLevel) {
            // TODO: Derive formulas for this
            int numberSpawned = 0;
            float budget = 5 + currentGameLevel;
            int chances = 5;
            while (budget > 0 && chances > 0) {
                // Try to spawn 
                var enemy = WeightedEnemyList.Next();
                if (budget - enemy.cost < 0) {
                    // give up if we couldn't spawn anything 5 times.
                    chances--;
                    continue;
                } else {
                    SpawnEnemy(Vector3.zero, enemy.prefab);
                    numberSpawned++;
                    budget-= enemy.cost;
                }
            }

        }

        private void SpawnEnemy(Vector3 position, GameObject prefab) {
            var obj = GlobalPool.Current.GetObject(prefab);
            obj.transform.position = position;
            obj.SetActive(true);
            var attr = obj.GetComponent<AttributeSet>();
            enemies.Add(attr.gameObject);
            attr.OnDeath += OnEnemyKilled;
            EnemyDisplayManager.Current.Register(attr);
        }

        private void OnEnemyKilled(AttributeSet attributeSet) {
            attributeSet.OnDeath -= OnEnemyKilled;
            enemies.Remove(attributeSet.gameObject);
            if (enemies.Count == 0) {
                OnEnemiesCleared?.Invoke();
            }

        }
    }
}