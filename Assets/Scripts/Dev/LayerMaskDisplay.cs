using UnityEngine;

public class LayerMaskDisplay : MonoBehaviour {
    public LayerMask mask;

    private void OnValidate() {
        Debug.Log(mask.value);
    }
}