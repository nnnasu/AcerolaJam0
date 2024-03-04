using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using Core.Abilities.Structures;
using UnityEngine;

public partial class PlayerController : MonoBehaviour {
    [SerializeField] InputReader input;
    [SerializeField] CharacterController characterController;
    [SerializeField] AbilityManager abilityManager;
    public float speed = 5; // TODO Use abilityManager.movementSpeed
    Vector2 currentInput;
    Vector3 movementDirection;
    public Vector3 mousePosition;
    Camera MainCamera;

    public LayerMask RecallLayer;

    private void Start() {
        MainCamera = Camera.main;
    }

    private void OnEnable() {
        input.OnMoveEvent += OnMove;
        input.OnFireEvent += OnFire;
        input.OnRecallEvent += OnRecall;
        input.OnSkillSelected += OnSkillSelected;
    }
    private void OnDisable() {
        input.OnMoveEvent -= OnMove;
        input.OnFireEvent -= OnFire;
        input.OnRecallEvent -= OnRecall;
        input.OnSkillSelected -= OnSkillSelected;
    }

    private void OnMove(Vector2 move) {
        currentInput = move;
    }

    private void OnFire() {
        abilityManager.OnClick(mousePosition);
    }

    private void OnRecall() {
        if (abilityManager.isSkillSelected) {
            DeselectSkill();
            return;
        }
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitinfo;
        if (Physics.Raycast(ray, out hitinfo, Mathf.Infinity, RecallLayer)) {
            var structure = hitinfo.collider.GetComponentInParent<StructureBase>();
            abilityManager.OnStructureRecall(structure);
        }
    }

    private void DeselectSkill() {
        abilityManager.SetActiveSkill(0);
    }

    private void OnSkillSelected(int index) {
        abilityManager.SetActiveSkill(index);
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
