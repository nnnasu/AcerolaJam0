using System.Collections;
using System.Collections.Generic;
using Core.Directors.Common;
using UnityEngine;

namespace Core.Directors.Rooms {
    public class EnemySpawnPoint : MonoBehaviour {

        public float radius;
        public EnemySpawnPointChannel channel;
        public bool RaiseImmediately = true;


        private void Start() {
            if (RaiseImmediately) channel.Raise(this);
        }

        public Vector3 GetRandomPosition() {
            var result = Random.insideUnitCircle * radius;
            return new(result.x, 0, result.y);
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        public void RequestSpawns() {
            channel.RequestAdditionalSpawns(this);
        }
    }
}