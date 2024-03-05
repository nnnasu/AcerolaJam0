
namespace Core.GlobalInfo {
    using UnityEngine;

    public class GameLevel : MonoBehaviour {
        public static GameLevel current;

        public int level { get; private set; } = 0;

        private void Awake() {
            if (current != null) Destroy(this);
            else current = this;
        }

        public void SetLevel(int lv) {
            level = lv;
        }


    }
}