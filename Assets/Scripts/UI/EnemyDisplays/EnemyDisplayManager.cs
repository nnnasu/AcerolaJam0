using System;
using System.Collections.Generic;
using UnityEngine;


public class EnemyDisplayManager : MonoBehaviour {
    public static EnemyDisplayManager Current;
    public Canvas canvas;
    public event Action OnUpdate = delegate { };
    Camera mainCam;

    public GameObject DisplayPrefab;
    private void Awake() {
        if (Current != null) Destroy(this);
        Current = this;
        mainCam = Camera.main;
    }

    public void Register(AttributeSet attributeSet) {
        var obj = GlobalPool.Current.GetObject(DisplayPrefab);
        var display = obj.GetComponent<EnemyDisplay>();
        display.gameObject.SetActive(true);
        display.transform.SetParent(canvas.transform, false);
        display.Bind(attributeSet, mainCam);

        OnUpdate += display.Tick;
        display.OnRelease += Unregister;
    }

    private void Unregister(EnemyDisplay display) {
        OnUpdate -= display.Tick;        
        display.OnRelease -= Unregister;
    }


    private void Update() {
        OnUpdate?.Invoke();
    }

}