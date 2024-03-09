using UnityEngine;
using System;


namespace Core.Directors.Rooms.Portals {
    public class TriggerEvent : MonoBehaviour {
        public event Action<Collider> OnTriggerEnterEvent = delegate { };
        private void OnTriggerEnter(Collider other) {
            OnTriggerEnterEvent?.Invoke(other);
        }
    }
}