
using UnityEngine;

namespace Core.Directors.Managers {
    public partial class GameStateManager : MonoBehaviour {
        // this is so fucking overengineered

        public int PlayerSemaphore { get; private set; } = 0;

        private void LockPlayer() {
            // Debug.Log($"Attempt to lock player, current = {PlayerSemaphore}");
            PlayerSemaphore++;
            if (PlayerSemaphore > 0) {
                playerController.enabled = false;
            }
        }

        private void UnlockPlayer() {
            // Debug.Log($"Attempt to unlock player, current = {PlayerSemaphore}");
            PlayerSemaphore--;
            if (PlayerSemaphore <= 0) {
                PlayerSemaphore = 0;
                playerController.enabled = true;
            }
        }


    }
}