using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities.Definitions;
using Core.Abilities.Instances;
using Core.Abilities.Structures;
using Core.Animation;
using PrimeTween;
using UnityEngine;

namespace Core.Abilities {
    public partial class AbilityManager : MonoBehaviour {
        public PlayerAttributeSet Attributes;
        public AnimationHandler AnimationHandler;
        public AttachmentHandler AttachmentHandler;
        public CharacterController characterController;
        public AudioSource SoundEffects;

        public Dictionary<StructureDefinition, StructureStorageInstance> StructureStorage = new();
        public event Action OnRebindRequest = delegate { };
        public event Action<StructureBase> OnStructureKilledEvent = delegate { };
        public event Action<AbilityInstance> OnCurrentSelectedAbilityChanged = delegate { };

        public float movementSpeed => Attributes.MovementSpeed;
        private int index = 0;
        public bool isSkillSelected => index != 0;
        public AbilityInstance CurrentlySelected { get; private set; }
        public Vector3 previousPosition; // before applying abilities



        public AbilityInstance BasicAttack;
        public List<AbilityInstance> Abilities = new(4);
        public ActionDefinition DefaultAttack;
        public DefaultAbility[] DefaultAbilities;


        public bool IsMovementControlledByAbility { get; private set; } = false;
        Tween MovementControlTween;


        private void Awake() {
            Initialize();
            CurrentlySelected = BasicAttack;
        }

        public void Initialize() {
            // TODO: Reset to base stats
            Abilities.Clear();

            for (int i = 0; i < 4; i++) {
                Abilities.Add(new(this));
            }
            BasicAttack = new(this);
            if (DefaultAttack) BasicAttack.actions.Add(new(DefaultAttack));
            for (int i = 0; i < DefaultAbilities.Length; i++) {
                var list = DefaultAbilities[i];
                foreach (var item in list.Actions) {
                    Abilities[i].actions.Add(new(item));
                }
            }

            Attributes.ResetState(true);
            RecalculateStats();
        }

        public void SetActiveSkill(int index) {
            Abilities.ForEach(x => x.SetFocus(false)); // clear current selected
            if (index == 0) {
                this.index = 0;
                CurrentlySelected = BasicAttack;
                OnCurrentSelectedAbilityChanged?.Invoke(CurrentlySelected);
                return;
            }

            int abilityIndex = index - 1;
            if (abilityIndex < Abilities.Count) {
                if (!Abilities[abilityIndex].SetFocus(true)) {
                    SetActiveSkill(0); // Reset to basic attack if we couldn't set focus.
                } else {
                    this.index = index;
                    CurrentlySelected = Abilities[abilityIndex];
                }
            }
            OnCurrentSelectedAbilityChanged?.Invoke(CurrentlySelected);
        }

        public void OnClick(Vector3 targetPosition) {
            previousPosition = transform.position;
            if (index == 0) {
                ActivateAbility(BasicAttack, targetPosition);
            } else {
                if (index - 1 >= Abilities.Count) return;

                var ability = Abilities[index - 1];
                ActivateAbility(ability, targetPosition);
            }
            SetActiveSkill(0);
        }

        public void OnStructureRecall(StructureBase structureBase, bool executeAction = true) {
            structureBase.OnRecall(this);
            if (StructureStorage.ContainsKey(structureBase.Definition)) {
                StructureStorage[structureBase.Definition].Remove(structureBase);
            }
        }

        public void OnStructureKilled(StructureBase structureBase) {
            OnStructureKilledEvent?.Invoke(structureBase);

        }


        private void ActivateAbility(AbilityInstance ability, Vector3 position) {
            if (ability == null) return;
            AnimationHandler.SetActionSpeed(1);
            if (ability.ActivateAbility(position)) {
                AnimationHandler.PlayAnimationState(ability.StateToPlay);                
            }
        }


        public void Teleport(Vector3 position) {
            characterController.enabled = false;
            transform.position = position;
            characterController.enabled = true;
        }

        public void TakeControlOfMovement(float time) {
            MovementControlTween.Stop();
            IsMovementControlledByAbility = true;
            MovementControlTween = Tween.Delay(time, RegainMovementControl);
        }

        private void RegainMovementControl() {
            IsMovementControlledByAbility = false;
        }

        public void MoveTick(Vector3 amount) {
            if (IsMovementControlledByAbility) return;
            characterController.Move(amount * Attributes.MovementSpeed);
            AnimationHandler.SetWalkAnimationDirection(amount);
        }
    }
}