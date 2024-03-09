using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Directors.Rooms {
    public class PlayerSpawnPoint : MonoBehaviour {

        public LocationChannel SpawnPointChannel;
        private void Start() {
            SpawnPointChannel.RaiseEvent(transform.position);
        }

    }
}