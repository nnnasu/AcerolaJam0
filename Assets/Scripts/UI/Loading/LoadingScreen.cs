using System;
using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Loading {
    public class LoadingScreen : MonoBehaviour {
        public CanvasGroup canvas;
        public Image loadingFill;
        Tween fadeTween;
        public float FadeInTime = 0.5f;
        public float ScreenStayTime = 3;
        public float FadeOutTime = 0.5f;

        public bool IsLoadingFinished { get; private set; } = false;
        private bool CanFadeOut = false;
        Func<float> GetProgress = null;

        public void ShowLoadingScreen(bool showInstantly = false) {
            gameObject.SetActive(true);
            IsLoadingFinished = false;
            CanFadeOut = false;
            loadingFill.fillAmount = 0;

            if (showInstantly) {
                canvas.alpha = 1;
                StartLoadingScreenStay();
                return;
            }

            fadeTween = Tween.Alpha(canvas, 0, 1, FadeInTime)
                .OnComplete(StartLoadingScreenStay);
        }

        public void SetProgressFunction(Func<float> GetProgress = null) {
            this.GetProgress = GetProgress;
        }

        private void StartLoadingScreenStay() {
            fadeTween = Tween.Delay(ScreenStayTime, AllowFadeOut);

        }

        public void SetLoadFinished() {
            IsLoadingFinished = true;
            loadingFill.fillAmount = 1;
            if (CanFadeOut) {
                // If SetLoadFinished() happened AFTER stay time completed.
                fadeTween = Tween.Alpha(canvas, 1, 0, FadeOutTime).OnComplete(Cleanup);
            }

        }

        private void AllowFadeOut() {
            if (IsLoadingFinished) {
                // If SetLoadFinished() happened before the Stay time completed.
                Tween.Alpha(canvas, 1, 0, FadeOutTime)
                    .OnComplete(Cleanup);
            }
            CanFadeOut = true;


        }


        private void Update() {
            if (GetProgress != null) {
                float progress = GetProgress.Invoke();
                loadingFill.fillAmount = progress;
            }
        }


        private void Cleanup() {
            gameObject.SetActive(false);
        }




    }
}