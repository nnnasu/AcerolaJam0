using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour {

    public Vector3 axis;
    public float turnRate;


    private void Update() {
        transform.Rotate(axis, turnRate * Time.deltaTime, Space.Self);
    }
}
