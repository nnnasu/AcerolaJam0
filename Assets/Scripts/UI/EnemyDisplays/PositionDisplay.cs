using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.PositionDisplays {
    public class PositionDisplay : PoolableBehaviour {

        /*
            Icon consists of :
            1. Base Image -- this is what's shown when on-screen
            2. Direction Indicator -- Arrow shown when off-screen, rotates
            3. Icon Indicator -- Icon that's shown when off-screen, does not rotate, can be empty.

        */

        [Header("UI Properties")]
        public Image OnScreenImage; // shown when on screen
        public Image directionIndicator; // shown when off-screen, rotates, default is facing up.
        public Image OffScreenImage; // shown when off-screen, does not rotate. Parent of direction Indicator
        public bool DisplayWhenOnScreen = true;
        public DisplayTargetSource target;
        public Vector3 offset => target.offset;
        protected Camera mainCamera;

        public event Action<PositionDisplay> OnRelease = delegate { };


        protected static bool IsPointVisibleOnScreen(Vector3 pos) {
            if (pos.x < 0 || pos.x > Screen.width) return false;
            if (pos.y < 0 || pos.y > Screen.height) return false;

            return true;
        }

        public virtual void Bind(DisplayTargetSource displayTargetSource, Camera mainCam) {
            target = displayTargetSource;
            mainCamera = mainCam;

            target.OnDisableEvent += Release;
        }

        public virtual void Tick() {
            var pos = mainCamera.WorldToScreenPoint(target.transform.position);            
            Vector3 uiPos = pos + offset;

            if (IsPointVisibleOnScreen(pos)) {
                // Set the position of the on-screen element. 
                if (!DisplayWhenOnScreen) OnScreenImage.gameObject.SetActive(false);
                else OnScreenImage.gameObject.SetActive(true);

                OnScreenImage.transform.position = uiPos;
                OffScreenImage.gameObject.SetActive(false);
                return;
            }

            // Target is not within the screen.
            OnScreenImage.gameObject.SetActive(false);
            OffScreenImage.gameObject.SetActive(true);

            Vector3 direction = uiPos - new Vector3(Screen.width / 2, Screen.height / 2);
            float angle = Vector3.SignedAngle(Vector3.up, direction.normalized, Vector3.forward);
            directionIndicator.transform.eulerAngles = new Vector3(0, 0, angle);


            float rectWidth = OffScreenImage.rectTransform.rect.width;
            float rectHeight = OffScreenImage.rectTransform.rect.height;
            float x = Mathf.Clamp(uiPos.x, rectWidth, Screen.width - rectWidth);
            float y = Mathf.Clamp(uiPos.y, rectHeight, Screen.height - rectHeight);
            //! BUG: When the position is far to the bottom left of the camera, the display appears top right.

            OffScreenImage.transform.position = new Vector3(x, y);
        }

        protected virtual void Release() {
            target.OnDisableEvent -= Release;
            if (this) ReturnToPool();
            OnRelease?.Invoke(this);
        }       


    }
}