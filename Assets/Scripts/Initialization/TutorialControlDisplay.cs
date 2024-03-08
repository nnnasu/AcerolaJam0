using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;

public class TutorialControlDisplay : MonoBehaviour {
    public CanvasGroup canvasGroup;
    public bool StartActivated = false;
    bool active;
    Tween tween;
    public float duration;

    private void Start() {
        active = StartActivated;

    }

    private void OnTriggerExit(Collider other) {
        if (other.tag != "Player") return;
        active = false;
        tween.Stop();
        tween = Tween.Alpha(canvasGroup, 1, 0, duration);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") return;
        if (!active) {
            active = true;
            tween.Stop();
            tween = Tween.Alpha(canvasGroup, 0, 1, duration);
        }
    }


}
