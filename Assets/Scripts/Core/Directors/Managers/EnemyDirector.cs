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
            SpawnPointChannel.OnAdditionalSpawnsRequested += SpawnExtra;
        }
        private void OnDisable() {
            SpawnPointChannel.OnSpawnPointRegistered -= SpawnEnemies;
            SpawnPointChannel.OnAdditionalSpawnsRequested -= SpawnExtra;
        }

        public void SetRoomParameters(RoomType room) {
            CurrentRoom = room;
        }

        private void SpawnEnemies(EnemySpawnPoint point) {
            SpawnEnemies(point, true);
        }

        private void SpawnEnemies(EnemySpawnPoint point, bool track = true) {
            int level = GameLevel.current.level;
            int spawns = CurrentRoom.GetSpawnCount(level);
            float budget = CurrentRoom.GetCredits(level);
            int chances = CurrentRoom.SpawnAttempts;
            List<AttributeSet> enemies = new();

            // Spawn Loop
            while (budget > 0 && chances > 0 && spawns > 0) {
                var enemy = CurrentRoom.SpawnList.GetRandomSpawn();
                if (budget - enemy.cost < 0) {
                    // give up if we couldn't spawn anything
                    chances--;
                    continue;
                } else {
                    var spawned = SpawnEnemy(point.GetRandomPosition(), enemy.prefab, track);
                    enemies.Add(spawned);
                    spawns--;
                    budget -= enemy.cost;
                }
            }

            // If we have excess budget, spend it by pulling modifiers to apply onto each enemy
            chances = CurrentRoom.SpawnAttempts;
            while (budget > 0 && chances > 0) {
                var mod = CurrentRoom.SpawnList.GetRandomMod();
                if (budget - mod.cost < 0) {
                    chances--;
                    continue;
                }
                int index = Mathf.FloorToInt(UnityEngine.Random.value * enemies.Count);
                var enemy = enemies[index];
                enemy.ApplyEffect(mod.Modification.GetEffectInstance(enemy, mod.GetLevel(level)));
                budget -= mod.cost;
            }


            if (track && enemies.Count == 0) OnEnemiesCleared?.Invoke(); // just to make sure we don't softlock the player if nothing spawns.

        }

        private void SpawnExtra(EnemySpawnPoint spawnPoint) {
            SpawnEnemies(spawnPoint, false);
        }

        private AttributeSet SpawnEnemy(Vector3 position, GameObject prefab, bool track = true) {
            var obj = GlobalPool.Current.GetObject(prefab);
            obj.transform.position = position;
            obj.SetActive(true);
            var attr = obj.GetComponent<AttributeSet>();
            if (track) {
                enemies.Add(attr.gameObject);
                attr.OnDeath += OnEnemyKilled;
            }
            return attr;
        }


        private void OnEnemyKilled(AttributeSet attributeSet) {
            attributeSet.OnDeath -= OnEnemyKilled;
            if (enemies.Contains(attributeSet.gameObject)) enemies.Remove(attributeSet.gameObject);
            if (enemies.Count == 0) {
                OnEnemiesCleared?.Invoke();
            }
        }

    }
}