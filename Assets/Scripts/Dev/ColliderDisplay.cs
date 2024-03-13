using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDisplay : MonoBehaviour {
    public Collider col;
    public Color wallColour = new Color(0, 1, 0, 0.5f);

    private void Reset() {
        if (!col) col = GetComponent<Collider>();
    }
    private void OnDrawGizmos() {
        Gizmos.color = wallColour;
        if (col is BoxCollider box) {
            Gizmos.DrawCube(transform.position + box.center, box.size);
        }
    }
}
