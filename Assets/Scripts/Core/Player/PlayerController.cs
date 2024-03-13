using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using Core.Abilities.Structures;
using Core.UI.Targeting;
using UnityEngine;

public partial class PlayerController : MonoBehaviour {
    [SerializeField] InputReader input;
    [SerializeField] public CharacterController characterController;
    [SerializeField] public AbilityManager abilityManager;
    public CursorManager cursorManager;
    Vector2 currentInput;
    Vector3 movementDirection;
    public Vector3 mousePosition;
    Camera MainCamera;

    public LayerMask RecallLayer;
    public LayerMask MousePositionLayer;

    public event Action<Vector3> OnMoveEvent = delegate { };
    public event Action OnControllerDisable = delegate { };

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
        OnControllerDisable?.Invoke();
        abilityManager?.MoveTick(Vector3.zero);

        currentInput = Vector2.zero;
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
        OnMoveEvent?.Invoke(movementDirection);
        abilityManager.MoveTick(movementDirection * Time.deltaTime);
        RegroundCharacter();
    }

    private void UpdateMouse() {
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitinfo;
        if (Physics.Raycast(ray, out hitinfo, Mathf.Infinity, MousePositionLayer)) {
            mousePosition = hitinfo.point;
        }

        RaycastHit structureRayInfo;
        if (Physics.Raycast(ray, out structureRayInfo, Mathf.Infinity, RecallLayer, QueryTriggerInteraction.Ignore)) {
            if (cursorManager) cursorManager.isHoveringOverStructure = true;
        } else {
            if (cursorManager) cursorManager.isHoveringOverStructure = false;
        }

        mousePosition.y = transform.position.y;
        transform.LookAt(mousePosition);
    }

    private void RegroundCharacter() {
        RaycastHit hit;
        Vector3 foot = characterController.bounds.center + ((characterController.height / 2) * Vector3.down);
        if (Physics.Raycast(foot, Vector3.down, out hit)) {
            Vector3 dist = hit.point - foot;
            characterController.Move(dist);
        }

    }
}
