using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController : MonoBehaviour {
    [SerializeField] InputReader input;
    [SerializeField] CharacterController characterController;
    [SerializeField] AbilityManager abilityManager;
    public float speed = 5;
    Vector2 currentInput;
    Vector3 movementDirection;
    Vector3 mousePosition;
    Camera MainCamera;

    private void Start() {
        MainCamera = Camera.main;
    }

    private void OnEnable() {
        input.OnMoveEvent += OnMove;
        input.OnFireEvent += OnFire;
    }
    private void OnDisable() {
        input.OnMoveEvent -= OnMove;
        input.OnFireEvent -= OnFire;
    }

    private void OnMove(Vector2 move) {
        currentInput = move;
    }

    private void OnFire() {
        abilityManager.OnClick(mousePosition);
    }

    private void Update() {
        UpdateMouse();
        movementDirection.x = currentInput.x;
        movementDirection.z = currentInput.y;
        characterController.Move(movementDirection * speed * Time.deltaTime);
    }

    private void UpdateMouse() {
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitinfo;
        if (Physics.Raycast(ray, out hitinfo)) {
            mousePosition = hitinfo.point;
        }        

        mousePosition.y = transform.position.y;
        transform.LookAt(mousePosition);
    }
}
