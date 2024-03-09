
using System;
using Core.Directors.Levels;
using UnityEngine;
namespace Core.Directors.Checkpoints {
    [CreateAssetMenu(fileName = "EventChannel", menuName = "Events/CheckpointRegistry", order = 0)]
    public class DoorEventChannel : ScriptableObject {

        public event Action<LevelInfo> OnLevelInfoRegistered = delegate { };
        public event Action<Checkpoint> OnCheckpointEntered = delegate { };

        public void RegisterCheckpoint(LevelInfo value) {
            OnLevelInfoRegistered?.Invoke(value);
        }

        public void EnterCheckpoint(Checkpoint value) {
            OnCheckpointEntered?.Invoke(value);
        }



    }
}