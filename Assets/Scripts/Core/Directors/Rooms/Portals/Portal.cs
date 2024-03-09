using System;
using System.Collections;
using System.Collections.Generic;
using Core.Utilities.Sounds;
using PrimeTween;
using UnityEngine;
using UnityEngine.VFX;

namespace Core.Directors.Rooms.Portals {
    public class Portal : MonoBehaviour {
        public Transform doorModel;
        Tween doorMovementTween;
        public Vector3 offset;
        public AudioSource audioPlayer;
        public SoundGroup DoorOpenSound;
        public float doorTiming;
        public VisualEffect vfx;

        public event Action<Collider> OnEntered = delegate { };
        public BoxCollider triggerVol;
        public TriggerEvent triggerer; // used to pass OnTriggerEnter() up to this component.

        public void SetColours(Color portal, Color particle) {
            vfx.SetVector4("PortalColour", portal);
            vfx.SetVector4("ParticleColour", particle);
        }

        private void OnEnable() {
            triggerer.OnTriggerEnterEvent += OnEnter;
        }
        private void OnDisable() {
            triggerer.OnTriggerEnterEvent -= OnEnter;
        }


        [ContextMenu("Show")]
        public void ShowPortal() {
            if (DoorOpenSound) audioPlayer.PlayOneShot(DoorOpenSound.GetRandomClip());
            doorMovementTween = Tween.LocalPosition(doorModel, offset, doorTiming, Ease.OutSine);
            doorMovementTween.OnComplete(EnableVFX);
        }

        private void EnableVFX() {
            vfx.enabled = true;
            triggerVol.enabled = true;
        }


        public void DisablePortal() {
            triggerVol.enabled = false;
        }


        // Test function to hide portals
        [ContextMenu("Hide")]
        public void HidePortal() {
            doorModel.localPosition = Vector3.zero;
            vfx.enabled = false;
            triggerVol.enabled = false;
        }



        private void OnEnter(Collider col) {
            OnEntered?.Invoke(col);
        }


    }
}