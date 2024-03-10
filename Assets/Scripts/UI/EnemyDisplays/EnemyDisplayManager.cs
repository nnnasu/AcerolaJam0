using System;
using System.Collections.Generic;
using Core.UI;
using UnityEngine;

namespace Core.UI.PositionDisplays {

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

        public void Register(DisplayTargetSource target) {
            var obj = GlobalPool.Current.GetObject(target.DisplayPrefab);
            var display = obj.GetComponent<PositionDisplay>();
            display.gameObject.SetActive(true);
            display.transform.SetParent(canvas.transform, false);
            display.Bind(target, mainCam);

            OnUpdate += display.Tick;
            display.OnRelease += Unregister;

        }



        private void Unregister(PositionDisplay display) {
            OnUpdate -= display.Tick;
            display.OnRelease -= Unregister;
        }


        private void Update() {
            OnUpdate?.Invoke();
        }

    }
}