using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Animation {
    using UnityEngine;

    [CreateAssetMenu(fileName = "AnimationStateInfo", menuName = "AnimationStateInfo", order = 0)]
    public class AnimationStateInfo : ScriptableObject {

        public string StateName;
        public int StateHash;
        public float UsageTime;
    
        [Tooltip("Lowest number is used first. ")]
        public int priority;

        private void OnValidate() {
            if (StateName.Length != 0) StateHash = Animator.StringToHash(StateName);
        }

    }
}
