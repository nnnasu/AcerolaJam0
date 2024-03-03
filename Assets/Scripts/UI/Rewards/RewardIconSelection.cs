using System;
using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.UI.Rewards {

    /// <summary>
    /// This class is used for individual icons and handles when the cursor hovers over or clicks the icon.
    /// </summary>
    public class RewardIconSelection : MonoBehaviour, IPointerMoveHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
        public int index;
        public bool isModifier;

        public event Action<int, bool, Vector2> OnHoverEvent = delegate { };
        public event Action<int, bool> OnClickEvent = delegate { };
        public event Action OnHoverLeft = delegate { };

        public Sprite defaultTexture;

        Tween selectionBorderTween;
        Tween hoverBorderTween;
        public Image SelectionBorder;
        public Image HoverBorder;
        public Image IconImage;

        bool isSelected;
        public float tweenDuration = 0.5f;


        public void OnPointerClick(PointerEventData eventData) {
            OnClickEvent?.Invoke(index, isModifier);
            isSelected = !isSelected;
            selectionBorderTween.Stop();
            if (isSelected) {
                selectionBorderTween = Tween.Custom(0, 1, tweenDuration, SetSelectionBorderAlpha);
            } else {
                selectionBorderTween = Tween.Custom(1, 0, tweenDuration, SetSelectionBorderAlpha);
            }
        }

        private void SetSelectionBorderAlpha(float value) {
            Color color = SelectionBorder.color;
            color.a = value;
            SelectionBorder.color = color;
        }
        private void SetHoverBorderAlpha(float value) {
            Color color = HoverBorder.color;
            color.a = value;
            HoverBorder.color = color;
        }

        public void OnPointerEnter(PointerEventData eventData) {
            OnHoverEvent?.Invoke(index, isModifier, eventData.position);
            hoverBorderTween.Stop();
            hoverBorderTween = Tween.Custom(0, 1, tweenDuration, SetHoverBorderAlpha);
        }

        public void OnPointerExit(PointerEventData eventData) {
            OnHoverLeft?.Invoke();
            hoverBorderTween.Stop();
            hoverBorderTween = Tween.Custom(1, 0, tweenDuration, SetHoverBorderAlpha);
        }

        public void OnPointerMove(PointerEventData eventData) {
            OnHoverEvent?.Invoke(index, isModifier, eventData.position);
        }

        public void SetIcon(Sprite icon = null) {
            if (!icon) {
                IconImage.sprite = defaultTexture;                
            } else {
                IconImage.sprite = icon;
            }

        }
    }
}