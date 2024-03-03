using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoverTipManager : MonoBehaviour {
    public TextMeshProUGUI tooltipText;
    public RectTransform tipWindow;

    public void ShowTip(string tip, Vector2 mousePos) {
        gameObject.SetActive(true);
        tooltipText.text = tip;
        // tipWindow.sizeDelta = new Vector2(tooltipText.preferredWidth > 200 ? 200 : tooltipText.preferredWidth, tooltipText.preferredHeight);
        tipWindow.position = mousePos;
        // TODO: Fix the overflow stuff (not important, is functional already)
        if (mousePos.x + tipWindow.rect.width >= Screen.width) {
            float overflow = Screen.width - mousePos.x;
            Vector2 newPos = tipWindow.position;
            newPos.x -= overflow;
            tipWindow.position = newPos;
        }
    }

    internal void HideTip() {
        tooltipText.text = "hide";
        gameObject.SetActive(false);
    }
}
