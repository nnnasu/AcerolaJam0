using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;


namespace Core.Animation {
    public class AttachmentSocket : MonoBehaviour {

        public PoolableBehaviour CurrentAttachment;
        Tween EffectExpiryTween;


        /// <summary>
        /// Removes the current attachment if any. 
        /// Sets the new attachment.
        /// </summary>
        /// <param name="newAttachment"></param>
        /// <param name="localOffset"></param>
        /// <param name="localRotationEuler"></param>
        public void ReplaceAttachment(PoolableBehaviour newAttachment, Vector3? localOffset = null, Vector3? localRotationEuler = null) {
            CurrentAttachment?.ReturnToPool();
            newAttachment.transform.SetParent(transform);
            newAttachment.transform.localPosition = Vector3.zero;
            newAttachment.transform.localRotation = Quaternion.identity;
            CurrentAttachment = newAttachment;
            newAttachment.gameObject.SetActive(true);

            if (localOffset.HasValue) {
                newAttachment.transform.localPosition = localOffset.Value;
            }

            if (localRotationEuler.HasValue) {
                newAttachment.transform.localEulerAngles = localRotationEuler.Value;
            }
        }

        public void ActivateEffects(bool activate, float? expiry = null) {
            EffectExpiryTween.Stop();
            if (CurrentAttachment is ActivatableAttachment activatable) {
                activatable.Deactivate();
                if (activate) activatable.Activate();
            }
            if (expiry.HasValue && expiry.Value > 0) {
                EffectExpiryTween = Tween.Delay(expiry.Value, TurnOffEffects);
            }
        }

        private void TurnOffEffects() {
            ActivateEffects(false);
        }



    }
}