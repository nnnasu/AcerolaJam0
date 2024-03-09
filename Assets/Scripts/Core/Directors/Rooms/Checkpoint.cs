using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using Core.Directors.Rooms.Portals;
using Core.Directors.Common;
using UnityEngine;


namespace Core.Directors.Rooms {
    public class Checkpoint : MonoBehaviour {
        public DoorEventChannel CheckpointRegister;
        public RoomType NextRoomType { get; internal set; } = null;
        public Portal portal;

        public void Enter(Collider other) {
            if (other.tag.Equals("Player")) {
                CheckpointRegister.EnterCheckpoint(this);
            }
        }

        private void OnEnable() {
            portal.OnEntered += Enter;
        }

        private void OnDisable() {
            portal.OnEntered -= Enter;
        }

        public void SetNextRoom(RoomType room) {
            NextRoomType = room;
            portal.SetColours(room.PortalColour, room.ParticleColour);
        }

        public void ShowDoor() {
            portal.ShowPortal();
        }

        public void DisablePortal() {
            portal.DisablePortal();
        }
    }
}