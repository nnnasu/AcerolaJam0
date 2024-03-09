using Core.Abilities;
using Core.Directors.Rooms;
using Core.Directors.Levels;
using Core.GlobalInfo;
using UnityEngine;
using UnityEngine.SceneManagement;
using Core.Directors.Common;

namespace Core.Directors.Managers {
    public partial class GameStateManager : MonoBehaviour {

        [Header("Player References")]
        public AbilityManager Player;
        public PlayerController playerController;

        [Header("Game Management")]
        public EnemyDirector EnemyDirector;
        public LocationChannel OnSpawnPointRegistered;
        public RoomLoader levelManager;
        public CheckpointManager checkpointManager;

        public int RoomsTraversed { get; private set; } = 0;

        [ContextMenu("Reset Game")]
        private void ReloadGame() {
            SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        }

        private void OnEnable() {
            rewardScreen.OnChoicesFinished += OnRewardCompleted;
            EnemyDirector.OnEnemiesCleared += ShowSwapUI;
            OnSpawnPointRegistered.Event += MovePlayerToLocation;
            checkpointManager.OnCheckpointEntered += LoadNextLevel;
            levelManager.OnLoadCompleted += OnLoadCompleted;
        }

        private void OnDisable() {
            rewardScreen.OnChoicesFinished -= OnRewardCompleted;
            EnemyDirector.OnEnemiesCleared -= ShowSwapUI;
            OnSpawnPointRegistered.Event -= MovePlayerToLocation;
            checkpointManager.OnCheckpointEntered -= LoadNextLevel;
            levelManager.OnLoadCompleted -= OnLoadCompleted;
        }

        private void MovePlayerToLocation(Vector3 point) {
            playerController.abilityManager.Teleport(point);
        }

        private void OnRewardCompleted() {
            HideSwapUI();
            checkpointManager.DistributeCheckpointTypes();
            checkpointManager.OpenDoors();
        }

        private void LoadNextLevel(RoomType room) {
            levelManager.LoadLevel(room);
            GameLevel.current.SetLevel(GameLevel.current.level + 1);
        }

        private void OnLoadCompleted(RoomType room) {
            EnemyDirector.SetRoomParameters(room);
        }

    }
}