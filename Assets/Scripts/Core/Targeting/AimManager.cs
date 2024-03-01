using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AimManager : MonoBehaviour {
    public DecalProjector projector;

    public void SetProjectorScale(Vector3 scale) {
        projector.size = scale;
        Vector3 pivot = projector.pivot;
        pivot.z = scale.z / 2 + 0.5f;
        projector.pivot = pivot;
    }

    public Vector3 scale;

    [ContextMenu("Set Scale")]
    public void SetScale() {
        SetProjectorScale(scale);
    }

    
}
