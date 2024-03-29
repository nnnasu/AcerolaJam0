
using System;
using Core.Directors.Rooms;
using UnityEngine;

namespace Core.Directors.Common {

    [CreateAssetMenu(fileName = "EnemySpawnPointChannel", menuName = "Events/EnemySpawnPointChannel", order = 0)]
    public class EnemySpawnPointChannel : ScriptableObject {
        public event Action<EnemySpawnPoint> OnSpawnPointRegistered = delegate { };
        public event Action<EnemySpawnPoint> OnAdditionalSpawnsRequested = delegate { };

        public void Raise(EnemySpawnPoint spawn) {
            OnSpawnPointRegistered?.Invoke(spawn);
        }

        public void RequestAdditionalSpawns(EnemySpawnPoint spawn, RoomType room = null) {
            // Support for changing the room parameters midway as well, may be useful for boss summoning more stuff.
            OnAdditionalSpawnsRequested?.Invoke(spawn);
        }

    }
}