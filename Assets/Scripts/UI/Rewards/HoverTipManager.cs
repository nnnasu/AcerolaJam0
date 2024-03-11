using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoverTipManager : MonoBehaviour {

    public TextMeshProUGUI TitleTextField;
    public TextMeshProUGUI LevelField;
    public TextMeshProUGUI DescriptionTextField;
    public RectTransform tipWindow;

    public void ShowTip(string title, string description, Vector2 mousePos, int level = 0) {
        gameObject.SetActive(true);
        TitleTextField.text = title;
        LevelField.text = level > 0 ? $"Level {level}" : "";
        DescriptionTextField.text = description;
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
        DescriptionTextField.text = "hide";
        gameObject.SetActive(false);
    }
}
