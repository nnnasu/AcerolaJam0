using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour {

    public LocationChannel SpawnPointChannel;
    private void Start() {
        SpawnPointChannel.RaiseEvent(transform.position);
    }

}
