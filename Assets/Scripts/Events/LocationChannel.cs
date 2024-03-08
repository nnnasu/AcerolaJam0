using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Location Channel", menuName = "Events/LocationChannel", order = 0)]
public class LocationChannel : ScriptableObject {
    public event Action<Vector3> Event = delegate { };

    public void RaiseEvent(Vector3 value) {
        Event?.Invoke(value);
    }
}
