using System;
using System.Collections.Generic;
using Core.Directors.Rooms;
using Core.Directors.Common;
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
            for (int i = 0; i < Count; i++) {
                Checkpoints[i].SetNextRoom(toDistribute[i]);
            }
        }

        private void Start() {
            CheckpointRegistry.RegisterCheckpoint(this);
        }

        public void OpenDoors() {
            Checkpoints.ForEach(x => x.ShowDoor());
        }

        public void DisableDoors() {
            Checkpoints.ForEach(x => x.DisablePortal());
        }
    }
}