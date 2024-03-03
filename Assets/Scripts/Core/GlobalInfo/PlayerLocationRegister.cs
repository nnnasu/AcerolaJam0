using UnityEngine;

namespace Core.GlobalInfo {
    public class PlayerLocationRegister : MonoBehaviour {

        bool started = false;

        private void Start() {
            started = true;
            OnEnable();
        }
        private void OnEnable() {
            if (started) PlayerLocation.CurrentLocator.RegisterPlayerLocation(gameObject);
        }

        private void OnDisable() {
            PlayerLocation.CurrentLocator.RegisterPlayerLocation(null);
        }


    }
}