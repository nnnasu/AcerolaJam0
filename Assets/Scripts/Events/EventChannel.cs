using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EventChannel", menuName = "EventChannel", order = 0)]
public class EventChannel : ScriptableObject {

    public event Action Event = delegate {};

    public void RaiseEvent() {
        Event?.Invoke();
    }
    
}