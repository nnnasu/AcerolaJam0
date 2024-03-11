using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities.Effects;
using Core.Abilities.Enums;
using Core.Abilities.Instances;
using Core.Animation;
using Core.AttributeSystem.Alignments;
using Core.Utilities.Scaling;
using PrimeTween;
using UnityEngine;



namespace Core.Abilities.Definitions {
    public abstract class ActionDefinition : ScriptableObject, IGetAlignmentLevel {
        [Header("Ability Parameters")]
        public ActionType actionType;
        public AlignmentDefinition alignment;

        public ScaledFloat DamageMultiplier;
        public ScaledFloat BaseCooldown;
        public ScaledFloat BaseMPCost;
        public List<OnHitEffect> OnHitEffects = new();
        public List<OnActivateEffect> OnActivateEffects = new();

        [Header("Animations")]
        public AnimationStateInfo AnimationToPlay;
        [Range(0, 1)]
        [Tooltip("Delay after starting the ability. Final delay is CastPoint * UsageTime")]
        public float CastPoint = 0;
        public float UsageTime = 0;

        [Header("Targeting Properties")]
        public TargetingType TargetingType;
        public Vector3 TargetSize;
        public Vector3 TargetOffset;
        public ScaledFloat Range;

        [Header("UI Properties")]
        public Sprite icon;
        public string ActionTitle;
        [TextArea] public string Description;



        public virtual void OnHit(AbilityManager owner, AbilityInstance ability, ActionInstance action, AttributeSet target) {
            OnHitEffects.ForEach(x => x.OnHit(owner, ability, action, target));
        }

        public void ActivateAction(AbilityManager owner, AbilityInstance ability, ActionInstance action, Vector3 target, Action<AttributeSet> OnHit = null) {
            OnActivateEffects.ForEach(x => x.OnActivate(owner, ability, action, OnHit));

            float animationMult = 1;
            if (ability.cachedUsageTime < ability.baseUsageTime && ability.cachedUsageTime > 0) {
                animationMult = ability.baseUsageTime / ability.cachedUsageTime;
            }
            owner.AnimationHandler.SetActionSpeed(animationMult);
            float delay = CastPoint * ability.cachedUsageTime;


            if (delay > 0) {
                Tween.Delay(delay, () => ActivateActionImplementation(owner, ability, action, target, OnHit));
            } else {
                ActivateActionImplementation(owner, ability, action, target, OnHit);
            }
        }

        public virtual string GetTooltipText(float level) {
            return Description;
        }

        public (AlignmentDefinition, int) GetAlignmentLevel(int level) {
            return (alignment, level * 2); // hardcoded value, but easy to turn into parameter
        }

        protected abstract void ActivateActionImplementation(AbilityManager owner, AbilityInstance ability, ActionInstance action, Vector3 target, Action<AttributeSet> OnHit = null);

    }
}