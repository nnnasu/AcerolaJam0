using UnityEngine;

public class ValueBarTester : MonoBehaviour {
    public ValueBar bar;
    

    public float MaxValue;
    public float CurrentValue;
    public float NewValue;

    [ContextMenu("Hit")]
    public void SetValue() {
        bar.OnValueChanged(CurrentValue, NewValue, MaxValue);

    }
}