using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoverTipManager : MonoBehaviour {
    public TextMeshProUGUI tooltipText;
    public RectTransform tipWindow;

    public void ShowTip(string tip, Vector2 mousePos) {
        Debug.Log(tip);
        tooltipText.text = tip;
        tipWindow.sizeDelta = new Vector2(tooltipText.preferredWidth > 200 ? 200 : tooltipText.preferredWidth, tooltipText.preferredHeight);
    }

    internal void HideTip() {
        tooltipText.text = "hide";
    }
}
