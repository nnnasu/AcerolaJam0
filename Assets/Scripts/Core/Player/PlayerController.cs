using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController : MonoBehaviour {
    [SerializeField] InputReader input;
    [SerializeField] CharacterController characterController;
    [SerializeField] AbilityManager abilityManager;
    public float speed = 5;
    Vector2 currentInput;
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
        input.OnFireEvent += OnFire;
    }

    private void OnMove(Vector2 move) {
        currentInput = move;
    }

    private void OnFire() {
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitinfo;
        if (Physics.Raycast(ray, out hitinfo)) {
            Debug.Log(hitinfo.point);
            Debug.DrawRay(hitinfo.point, Vector3.up * 5, Color.red, 3);
            abilityManager.OnClick(hitinfo.point);
        }
        


    }

    private void Update() {
        characterController.Move(new Vector3(currentInput.x, 0, currentInput.y) * speed * Time.deltaTime);
    }
}
