using UnityEngine;
using System;


namespace Core.Directors.Checkpoints.Portals {
    public class TriggerEvent : MonoBehaviour {
        public event Action<Collider> OnTriggerEnterEvent = delegate { };
        private void OnTriggerEnter(Collider other) {
            OnTriggerEnterEvent?.Invoke(other);
        }
    }
}