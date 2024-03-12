using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Animation {

    public enum AttachmentSlot {
        RightHand,
        LeftHand,
        Head,
        Hips
    }
    public class AttachmentHandler : MonoBehaviour {
        
        public AttachmentSocket RightHand;
        public AttachmentSocket LeftHand;
        public AttachmentSocket Head;
        public AttachmentSocket Hips;        

    }
}