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
    public class AbilitySlotSelection : MonoBehaviour, IPointerMoveHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
        public int index;
        public bool isModifier;

        public event Action<int, bool, Vector2, bool> OnHoverEvent = delegate { };
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
        public bool IsHoveredOver = false;


        public void OnPointerClick(PointerEventData eventData) {
            OnClickEvent?.Invoke(index, isModifier);
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

        public void SetSelected(bool select) {
            isSelected = select;
            selectionBorderTween.Stop();
            if (isSelected) {
                selectionBorderTween = Tween.Custom(0, 1, tweenDuration, SetSelectionBorderAlpha);
            } else {
                selectionBorderTween = Tween.Custom(1, 0, tweenDuration, SetSelectionBorderAlpha);
            }
        }

        public void OnPointerEnter(PointerEventData eventData) {
            OnHoverEvent?.Invoke(index, isModifier, eventData.position, IsHoveredOver);
            IsHoveredOver = true;
            hoverBorderTween.Stop();
            hoverBorderTween = Tween.Custom(0, 1, tweenDuration, SetHoverBorderAlpha);
        }

        public void OnPointerExit(PointerEventData eventData) {
            OnHoverLeft?.Invoke();
            IsHoveredOver = false;
            hoverBorderTween.Stop();
            hoverBorderTween = Tween.Custom(1, 0, tweenDuration, SetHoverBorderAlpha);
        }

        public void OnPointerMove(PointerEventData eventData) {
            OnHoverEvent?.Invoke(index, isModifier, eventData.position, IsHoveredOver);
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