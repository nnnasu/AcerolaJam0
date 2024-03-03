using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.GlobalInfo {
    public class PlayerLocation : MonoBehaviour {
        public static PlayerLocation CurrentLocator;
        private GameObject player;
        public Vector3? playerLocation => player?.transform.position;



        private void Awake() {
            if (CurrentLocator != null) Destroy(this);
            CurrentLocator = this;
        }

        public void RegisterPlayerLocation(GameObject obj) {
            player = obj;
        }




    }
}