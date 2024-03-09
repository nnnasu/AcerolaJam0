using System;
using System.Collections.Generic;
using System.Linq;
using Core.Directors.Levels;
using UnityEngine;


namespace Core.Directors.Checkpoints {
    /// <summary>
    /// GameManager class which abstracts checkpoint events.
    /// </summary>
    public class CheckpointManager : MonoBehaviour {
        public DoorEventChannel registry;
        public LevelInfo ActiveLevel { get; private set; } = null;
        public event Action<RoomType> OnCheckpointEntered = delegate { };
        private System.Random random = new();

        public List<RoomType> typesToDistribute = new();
        public RoomType bossRoom;

        /// <summary>
        /// This adds the boss room into the list of types available. 
        /// Since we're just using orderby method of assigning types. 
        /// </summary>
        public void EnableBossRoom() {
            typesToDistribute.Add(bossRoom);
        }

        public void DistributeCheckpointTypes() {
            int count = ActiveLevel.Count;
            List<RoomType> toDistribute = typesToDistribute
                .OrderBy(x => random.Next())
                .Take(count)
                .ToList();
            ActiveLevel.DistributeCheckpointTypes(toDistribute);
        }

        private void OnEnable() {
            ActiveLevel = null;
            registry.OnLevelInfoRegistered += RegisterLevelInfo;
            registry.OnCheckpointEntered += OnCheckpointEnter;
        }

        private void OnDisable() {
            ActiveLevel = null;
            registry.OnLevelInfoRegistered -= RegisterLevelInfo;
            registry.OnCheckpointEntered -= OnCheckpointEnter;
        }

        public void ClearCheckpoints() {
            ActiveLevel = null;
        }



        private void RegisterLevelInfo(LevelInfo level) {
            ActiveLevel = level;
        }


        private void OnCheckpointEnter(Checkpoint checkpoint) {
            OnCheckpointEntered?.Invoke(checkpoint.NextRoomType);
            ClearCheckpoints();
        }
    }
}