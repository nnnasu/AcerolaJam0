using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using UnityEngine.VFX;

public class MaterialColourChanger : MonoBehaviour {

    public MeshRenderer render;
    public Material baseMat;
    public Material lerpMat;
    public float duration = 2;

    public VisualEffect vfx;

    private void Start() {
        render.material = baseMat;        
    }

    private void Update() {
        float lerp  = Mathf.PingPong(Time.time, duration) / duration;
        render.material.Lerp(baseMat, lerpMat, lerp);
    }
}
