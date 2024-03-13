using System.Collections;
using System.Collections.Generic;
using Core.Animation;
using Core.Enemies.Boss.Actions;
using Core.GlobalInfo;
using PrimeTween;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Enemies.Boss {

    public enum BossStates {
        Idle,
        Acting,
        Resting // Right after an action, don't do anything for a bit.
    }
    public partial class BossAIController : MonoBehaviour {


        public AttributeSet attributes;

        [Header("Movement")]
        public CharacterController characterController;
        public float TurnRate = 1;
        public float desiredDistance = 6;
        public float distanceSoftRange = 7; // i.e. at 1m or 11m, travel at max speed, otherwise lerp.
        public float deadzone = 2;

        public AnimationHandler animationHandler;
        public Animator animator;



        // State
        public HashSet<BossAction> Cooldowns = new();
        public BossStates state = BossStates.Idle;
        public Vector3 cachedPlayerLocation;
        public bool CanTurn = true;
        public bool CanMove = true;
        public MovementStrategy CurrentMovementStrategy;

        public ActionWeightedList actions;
        public MovementWeightedList movements;
        public int ActionAttempts = 3;
        public float WaitTime = 2;

        Tween actionTween;

        public UnityEvent OnSupportRequested;
        public UnityEvent OnKilled;

        private void Start() {
            CanMove = false;
            CanTurn = false;
            actionTween = Tween.Delay(5, Idle);
        }

        private void OnEnable() {
            animator.SetBool("HasCombatStarted", true);
        }

        private void Idle() {
            attributes.IsInvulnerable = false;
            CanMove = CanTurn = true;
            state = BossStates.Idle;
            float wait = Random.value * WaitTime;
            CurrentMovementStrategy = movements.GetMovement();

            Tween.Delay(wait, Act);
        }

        private void Act() {
            float wait = Random.value * WaitTime;
            float actionTime = 0;
            for (int i = 0; i < ActionAttempts; i++) {
                var act = actions.GetAction();
                if (!act.CanExecute(this)) continue;
                CanMove = false;
                actionTime = act.Execute(this);
                break;
            }

            Tween.Delay(wait, Idle);
        }
        private void Update() {
            UpdatePlayerLocation();

            MoveAndRotate(Time.deltaTime);

        }

        private void UpdatePlayerLocation() {
            if (PlayerLocation.CurrentLocator.playerLocation.HasValue) cachedPlayerLocation = PlayerLocation.CurrentLocator.playerLocation.Value;
        }


    }
}