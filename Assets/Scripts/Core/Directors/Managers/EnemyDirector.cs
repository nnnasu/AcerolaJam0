using System;
using System.Collections.Generic;
using UnityEngine;
using Core.Directors.Common;
using Core.Directors.Rooms;
using Core.GlobalInfo;

namespace Core.Directors.Managers {
    public class EnemyDirector : MonoBehaviour {
        public event Action OnEnemiesCleared = delegate { };
        public RoomType CurrentRoom; // Set to a default room type.
        public EnemySpawnPointChannel SpawnPointChannel;

        private Dictionary<EnemySpawnParameters, int> EnemySpawnCount = new();
        private HashSet<GameObject> enemies = new();

        private void OnEnable() {
            SpawnPointChannel.OnSpawnPointRegistered += SpawnEnemies;
        }
        private void OnDisable() {
            SpawnPointChannel.OnSpawnPointRegistered -= SpawnEnemies;
        }

        public void SetRoomParameters(RoomType room) {
            CurrentRoom = room;
        }

        private void SpawnEnemies(EnemySpawnPoint point) {
            int level = GameLevel.current.level;
            int spawns = CurrentRoom.GetSpawnCount(level);
            float budget = CurrentRoom.GetCredits(level);
            int chances = CurrentRoom.SpawnAttempts;

            // Spawn Loop
            while (budget > 0 && chances > 0 && spawns > 0) {
                var enemy = CurrentRoom.SpawnList.GetRandomSpawn();
                if (budget - enemy.cost < 0) {
                    // give up if we couldn't spawn anything
                    chances--;
                    continue;
                } else {
                    SpawnEnemy(point.GetRandomPosition(), enemy.prefab);
                    spawns--;
                    budget -= enemy.cost;
                }
            }

            if (enemies.Count == 0) OnEnemiesCleared?.Invoke(); // just to make sure we don't softlock the player if nothing spawns.

        }
        private void SpawnEnemy(Vector3 position, GameObject prefab) {
            var obj = GlobalPool.Current.GetObject(prefab);
            obj.transform.position = position;
            obj.SetActive(true);
            var attr = obj.GetComponent<AttributeSet>();
            enemies.Add(attr.gameObject);
            attr.OnDeath += OnEnemyKilled;
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