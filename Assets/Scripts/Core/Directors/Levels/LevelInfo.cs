using System;
using System.Collections.Generic;
using Core.Directors.Checkpoints;
using UnityEngine;

namespace Core.Directors.Levels {
    /// <summary>
    /// Per-level object which holds all information about the room's spawning/progression logic. 
    /// </summary>
    public class LevelInfo : MonoBehaviour {

        public DoorEventChannel CheckpointRegistry;
        public List<Checkpoint> Checkpoints = new();
        public int Count => Checkpoints.Count;

        internal void DistributeCheckpointTypes(List<RoomType> toDistribute) {
            throw new NotImplementedException();
        }

        private void OnEnable() {
            CheckpointRegistry.RegisterCheckpoint(this);
        }
    }
}