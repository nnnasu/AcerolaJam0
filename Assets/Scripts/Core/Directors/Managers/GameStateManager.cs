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


        [Header("Initialization")]
        public bool LoadTutorialOnStart = true;
        public RoomType TutorialRoom;

        [ContextMenu("Reset Game")]
        private void ReloadGame() {
            SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        }

        
        private void Start() {
            // Load the first level
            if (LoadTutorialOnStart) LoadNextLevel(TutorialRoom, true);
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
            LoadNextLevel(room, false);
        }

        private void LoadNextLevel(RoomType room, bool skipFade = false) {
            LockPlayer();
            levelManager.LoadLevel(room, skipFade);
            GameLevel.current.SetLevel(GameLevel.current.level + 1);
        }

        private void OnLoadCompleted(RoomType room) {
            UnlockPlayer();
            EnemyDirector.SetRoomParameters(room);
        }

    }
}