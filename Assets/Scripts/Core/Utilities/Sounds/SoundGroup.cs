using UnityEngine;
namespace Core.Utilities.Sounds {

    [CreateAssetMenu(fileName = "SoundGroup", menuName = "SoundGroup", order = 0)]
    public class SoundGroup : ScriptableObject {
        public AudioClip[] sounds;

        public AudioClip GetRandomClip() {
            if (sounds.Length == 0) return null;
            int index = Mathf.FloorToInt(Random.value * sounds.Length);
            return sounds[index];
        }

    }
}