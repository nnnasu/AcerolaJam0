using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using Core.Abilities.Definitions;
using Core.Abilities.Effects;
using Core.Abilities.Instances;
using PrimeTween;
using UnityEngine;



namespace Core.AbilityExtensions.Actions {
    [CreateAssetMenu(fileName = "Movement Action", menuName = "Ability System/Player Actions/Movement", order = 0)]
    public class MovementAction : ActionDefinition {

        public class ControllerDirection {
            public CharacterController characterController;
            public Vector3 directionWS;
        }

        [Header("Movement Options")]
        public float MinimumDistance = 4;
        public float TravelTime = 1;
        public Ease ease;

        [Header("Landing Effects")]
        public List<OnActivateEffect> OnLandEffects = new();


        protected override void ActivateActionImplementation(AbilityManager owner, AbilityInstance ability, ActionInstance action, Vector3 target, Action<AttributeSet> OnHit = null) {
            Vector3 direction = target - owner.transform.position;
            direction.y = 0;
            float magnitude = direction.magnitude;
            direction.Normalize();

            Vector3 targetPoint = owner.transform.position + Mathf.Max(MinimumDistance, Mathf.Min(magnitude, Range.GetValueAtLevel(action.level))) * direction;


            owner.TakeControlOfMovement(TravelTime);
            ControllerDirection controllerDirection = new() {
                characterController = owner.characterController,
                directionWS = direction
            };

            // Tween.Delay(UsageTime, () => OnLanding(owner, ability, action, target, OnHit))
            //     .OnUpdate(controllerDirection, MoveController);
            Tween.Custom(owner.transform.position, targetPoint, TravelTime, (val) => {
                Vector3 delta = val - owner.transform.position;
                owner.characterController.Move(delta);
            }, ease)
            .OnComplete(() => OnLanding(owner, ability, action, target, OnHit));

        }

        protected void MoveController(ControllerDirection con, Tween tween) {
            // TODO

            float speed = 5;
            con.characterController.Move(con.directionWS * speed * Time.deltaTime);
        }

        protected void OnLanding(AbilityManager owner, AbilityInstance ability, ActionInstance action, Vector3 target, Action<AttributeSet> OnHit = null) {

            OnLandEffects.ForEach(x => x.OnActivate(owner, ability, action, OnHit));
        }

    }
}
