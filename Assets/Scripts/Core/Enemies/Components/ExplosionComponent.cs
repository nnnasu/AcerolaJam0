using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using UnityEngine.VFX;

namespace Core.Enemies.Components {

    public enum ChargeStatus {
        Neutral,
        Charging,
        Charged
    }
    public class ExplosionComponent : MonoBehaviour {

        public MeshRenderer render;
        public Material baseMat;
        public Material lerpMat;

        public ChargeStatus ChargeStatus { get; private set; } = ChargeStatus.Neutral;

        public float ChargeTimer = 2;
        public float DefuseTime = 5;
        public float ExplosionDelay = 2;

        Tween chargeTween;
        Tween ShakeTween;
        float currentLerp = 0;
        public VisualEffect vfx;

        public ShakeSettings ChargedShakeSettings;
        private bool MarkedForDetonation = false;


        private void OnEnable() {
            render.material = baseMat;
            MarkedForDetonation = false;
        }

        public void StartCharge() {
            vfx.enabled = true;
            chargeTween = Tween.Custom(0, 1, ChargeTimer, LerpMaterial)
                .OnComplete(OnChargeComplete);
            ChargeStatus = ChargeStatus.Charging;

        }
        private void OnChargeComplete() {
            vfx.enabled = false;
            ChargedShakeSettings.duration = ExplosionDelay;
            ShakeTween = Tween.ShakeLocalPosition(transform, ChargedShakeSettings)
                .OnComplete(OnShakeComplete); // If the explosion action doesn't trigger again (not in range), return
            ChargeStatus = ChargeStatus.Charged;
        }

        private void OnShakeComplete() {
            if (MarkedForDetonation) return;
            CancelCharge();

        }

        public void CancelCharge() {
            ChargeStatus = ChargeStatus.Neutral;
            ShakeTween.Stop();
            chargeTween.Stop();
            float lerpTime = currentLerp * DefuseTime;
            chargeTween = Tween.Custom(currentLerp, 0, lerpTime, LerpMaterial);
        }


        private void LerpMaterial(float value) {
            render.material.Lerp(baseMat, lerpMat, value);
            currentLerp = value;
        }

        public float GetRemainingChargeTime() {
            if (chargeTween.isAlive) {
                return chargeTween.duration - chargeTween.elapsedTime;
            }
            return 0;
        }

        public void MarkForDetonation() {
            MarkedForDetonation = true;
        }



    }
}