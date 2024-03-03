using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverReceiver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler, IPointerClickHandler {
    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log("clicked");
    }

    public void OnPointerEnter(PointerEventData eventData) {
        Debug.Log("hover enter");
    }

    public void OnPointerExit(PointerEventData eventData) {
        Debug.Log("hover exit");
    }

    public void OnPointerMove(PointerEventData eventData) {
        Debug.Log("move");
    }
}
