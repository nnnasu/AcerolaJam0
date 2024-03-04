using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


// [CreateAssetMenu(fileName = "InputReader", menuName = "InputReader", order = 0)]
public class InputReader : ScriptableObject, InputActions.IGameMapActions {

    private InputActions input;

    public event Action<Vector2> OnMoveEvent = delegate { };
    public event Action OnFireEvent = delegate { };
    public event Action OnRecallEvent = delegate { };
    public event Action<int> OnSkillSelected = delegate {};


    public void OnMove(InputAction.CallbackContext context) {
        OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
    }
    public void OnFire(InputAction.CallbackContext context) {
        if (context.performed) OnFireEvent?.Invoke();
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

    public void SelectSkill(int num) {
        OnSkillSelected?.Invoke(num);
    }

    public void OnSelectSkillA(InputAction.CallbackContext context) {
        SelectSkill(1);        
    }

    public void OnSelectSkillB(InputAction.CallbackContext context) {
        SelectSkill(2);
    }

    public void OnSelectSkillC(InputAction.CallbackContext context) {
        SelectSkill(3);
    }

    public void OnSelectSkillD(InputAction.CallbackContext context) {
        SelectSkill(4);
    }
}