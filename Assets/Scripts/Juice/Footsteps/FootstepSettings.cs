using System;
using System.Collections.Generic;
using Core.Utilities.Sounds;
using UnityEngine;


namespace Juice.Footsteps {
    [Serializable]
    public class SoundMapping {
        public string tag;
        public SoundGroup soundGroup;
    }

    [CreateAssetMenu(fileName = "FootstepSettings", menuName = "FootstepSettings", order = 0)]
    public class FootstepSettings : ScriptableObject {

        public LayerMask RaycastSettings = 8 | 16;
        public Dictionary<string, SoundGroup> sounds;
        public List<SoundMapping> mappings;
        public bool UseFallback = true;
        public SoundGroup fallbackSounds;

        private void OnEnable() {
            sounds = new();
            mappings.ForEach(x => sounds.Add(x.tag, x.soundGroup));
        }

        public AudioClip GetAudio(string tag) {
            if (sounds.ContainsKey(tag)) return sounds[tag].GetRandomClip();
            if (UseFallback) return fallbackSounds.GetRandomClip();
            return null;
        }


    }
}