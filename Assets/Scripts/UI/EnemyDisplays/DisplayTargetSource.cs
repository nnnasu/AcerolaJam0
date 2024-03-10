using System;
using UnityEngine;

namespace Core.UI.PositionDisplays {
    public class DisplayTargetSource : MonoBehaviour {

        public GameObject DisplayPrefab;
        public Vector3 offset;

        public event Action OnDisableEvent = delegate { };

        private void OnEnable() {
            Register();

        }

        private void OnDisable() {
            Unregister();
        }
        
        private void Register() {
            EnemyDisplayManager.Current.Register(this);
        }

        private void Unregister() {
            OnDisableEvent?.Invoke();

        }
    }
}