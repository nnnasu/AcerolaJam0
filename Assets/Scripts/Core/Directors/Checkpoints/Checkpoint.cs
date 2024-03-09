using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using UnityEngine;


namespace Core.Directors.Checkpoints {
    public class Checkpoint : MonoBehaviour {
        public DoorEventChannel CheckpointRegister;
        public RoomType NextRoomType { get; internal set; } = null;

        public void Enter(Collider other) {
            if (other.GetComponent<AbilityManager>()) {
                CheckpointRegister.EnterCheckpoint(this);
            }
        }

        public void SetNextRoom(RoomType room) {
            NextRoomType = room;


        }
    }
}