using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimationHandler : MonoBehaviour {

    public static int MoveXHash = Animator.StringToHash("MoveX");
    public static int MoveYHash = Animator.StringToHash("MoveY");


    public Animator animator;
    public PlayerController controller;

    private void OnEnable() {
        controller.OnControllerDisable += OnControllerDisableEvent;
        controller.OnMoveEvent += OnInputDirectionChange;
    }
    private void OnDisable() {
        controller.OnControllerDisable -= OnControllerDisableEvent;
        controller.OnMoveEvent -= OnInputDirectionChange;
    }
    public void OnControllerDisableEvent() {
        animator.SetFloat(MoveXHash, 0);
        animator.SetFloat(MoveYHash, 0);
    }

    public void OnInputDirectionChange(Vector3 input) {
        var relativeRot = Quaternion.FromToRotation(transform.forward, Vector3.forward);
        Vector3 localDirection = (relativeRot * input).normalized;
        animator.SetFloat(MoveXHash, localDirection.x);
        animator.SetFloat(MoveYHash, localDirection.z);
    }


}
