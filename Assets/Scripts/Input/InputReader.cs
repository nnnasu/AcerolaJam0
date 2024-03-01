using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName = "InputReader", menuName = "InputReader", order = 0)]
public class InputReader : ScriptableObject, InputActions.IGameMapActions {

    private InputActions input;

    public event Action<Vector2> OnMoveEvent = delegate { };
    public event Action OnFireEvent = delegate { };
    public event Action OnRecallEvent = delegate { };


    public void OnMove(InputAction.CallbackContext context) {
        OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
    }
    public void OnFire(InputAction.CallbackContext context) {
        OnFireEvent?.Invoke();
    }
    public void OnRecall(InputAction.CallbackContext context) {
        OnRecallEvent?.Invoke();
    }



    private void OnEnable() {
        if (input == null) {
            input = new InputActions();
        }
        input.GameMap.SetCallbacks(this);
        input.GameMap.Enable();
    }

    private void OnDisable() {
        input.GameMap.Disable();
        input.GameMap.RemoveCallbacks(this);
    }

}