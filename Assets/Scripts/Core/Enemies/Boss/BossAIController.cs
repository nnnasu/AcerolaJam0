using System.Collections;
using System.Collections.Generic;
using Core.Animation;
using Core.Enemies.Boss.Actions;
using Core.GlobalInfo;
using PrimeTween;
using UnityEngine;

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


        // State
        public HashSet<BossAction> Cooldowns = new();
        public BossStates state = BossStates.Idle;
        public Vector3 cachedPlayerLocation;

        public ActionWeightedList actions;
        public int ActionAttempts = 3;
        public float WaitTime = 2;

        Tween actionTween;

        private void Start() {

            actionTween = Tween.Delay(WaitTime * Random.value, Act);
        }

        private void Idle() {
            state = BossStates.Idle;
            float wait = Random.value * WaitTime;

            Tween.Delay(wait, Act);
        }

        private void Act() {
            float wait = Random.value * WaitTime;
            float actionTime = 0;
            for (int i = 0; i < ActionAttempts; i++) {
                var act = actions.GetAction();
                if (!act.CanExecute(this)) continue;
                actionTime = act.Execute(this);
                break;
            }
            
            Tween.Delay(wait, Idle);
        }
        private void Update() {
            UpdatePlayerLocation();

            switch (state) {
                case BossStates.Idle:
                    MaintainPlayerDistanceAndOrientation(Time.deltaTime);
                    break;
                case BossStates.Resting: break;
                default: break;
            }

        }

        private void UpdatePlayerLocation() {
            if (PlayerLocation.CurrentLocator.playerLocation.HasValue) cachedPlayerLocation = PlayerLocation.CurrentLocator.playerLocation.Value;
        }


        // private void Update() {
        //     if (PlayerLocation.CurrentLocator.playerLocation.HasValue) cachedPlayerLocation = PlayerLocation.CurrentLocator.playerLocation.Value;

        //     switch (state) {
        //         case BossStates.Idle:
        //             MaintainPlayerDistanceAndOrientation(Time.deltaTime);
        //             RegroundCharacter();
        //             break;
        //         case BossStates.Acting: break;
        //         case BossStates.Resting: break;

        //         default: break;
        //     }
        // }

    }
}