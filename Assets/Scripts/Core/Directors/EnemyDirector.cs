using System;
using System.Collections;
using System.Collections.Generic;
using Core.GlobalInfo;
using UnityEngine;

namespace Core.Directors {
    public class EnemyDirector : MonoBehaviour {
        private HashSet<GameObject> enemies = new();
        public event Action OnEnemiesCleared = delegate { };
        public List<GameObject> Enemies = new();
        public GameObject CheckpointPrefab;

        private void Start() {
            SpawnCheckpoint();
        }

        public void SpawnCheckpoint() {
            // TODO: Use this to spawn additional checkpoint types
            var pt = GlobalPool.Current.GetObject(CheckpointPrefab);
            pt.SetActive(true);
            pt.transform.position = new Vector3(0, 1, 5);
        }

        public void SpawnEnemies(float credits, int currentGameLevel) {
            Enemies.ForEach(x => SpawnEnemy(Vector3.up, x));
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