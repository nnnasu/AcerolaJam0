using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EventChannel", menuName = "EventChannel", order = 0)]
public class EventChannel : ScriptableObject {

    public event Action<int> Event = delegate {};

    public void RaiseEvent(int value) {
        Event?.Invoke(value);
    }
    
}