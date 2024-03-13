using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Abilities.Effects;
using Core.Abilities.Enums;
using Core.Abilities.Instances;
using Core.Animation;
using Core.AttributeSystem;
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
        public EntityType IgnoredEntities = EntityType.Player;

        public List<OnHitEffect> OnHitUniqueEffects = new();
        public List<OnHitEffect> OnHitEffects = new();
        public List<OnActivateEffect> OnActivateEffects = new();


        [Header("Animations")]
        public AnimationStateInfo AnimationToPlay;
        [Range(0, 1)]
        [Tooltip("Delay after starting the ability. Final delay is CastPoint * UsageTime")]
        public float CastPoint = 0;
        [SerializeField] float BackupUsageTime;
        public float UsageTime => AnimationToPlay ? AnimationToPlay.UsageTime : BackupUsageTime;

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
            OnHitEffects
                .ForEach(x => x.OnHit(owner, ability, action, target));
        }

        public virtual void OnActionHit(AbilityManager owner, AbilityInstance ability, ActionInstance action, AttributeSet target) {
            OnHitUniqueEffects.ForEach(x => x.OnHit(owner, ability, action, target));
        }

        public void ActivateAction(AbilityManager owner, AbilityInstance ability, ActionInstance action, Vector3 target, Action<AttributeSet> OnHit = null, Action<AttributeSet> OnActionHit = null) {

            float animationMult = 1;
            if (ability.cachedUsageTime < ability.baseUsageTime && ability.cachedUsageTime > 0) {
                animationMult = ability.baseUsageTime / ability.cachedUsageTime;
            }
            owner.AnimationHandler.SetActionSpeed(animationMult);
            float delay = CastPoint * ability.cachedUsageTime;

            Action<AttributeSet> combinedAction = delegate { };
            if (OnHit != null) combinedAction += OnHit;
            if (OnActionHit != null) combinedAction += OnActionHit;


            if (delay > 0) {
                Tween.Delay(delay, () => DelayedActivate(owner, ability, action, target, combinedAction));
            } else {
                OnActivateEffects.ForEach(x => x.OnActivate(owner, ability, action, combinedAction));
                ActivateActionImplementation(owner, ability, action, target, combinedAction);
            }
        }

        public string GetTooltipText(int level) {
            List<string> results = new();
            if (alignment) results.Add(alignment.GetTooltipText(GetAlignmentLevel(level).Item2));
            results.Add(GetActionDescription(level));

            OnHitEffects.ForEach(x => results.Add(x.GetTooltip(level)));
            OnActivateEffects.ForEach(x => results.Add(x.GetTooltip(level)));
            return string.Join("\n", results);
        }

        public virtual string GetActionDescription(int level) {
            StringBuilder sb = new(Description);

            //* Replaces specific strings in the description with the following. 
            sb.Replace("{RANGE}", $"{Range.GetValueAtLevel(level)}m");
            sb.Replace("{DAMAGE}", $"{DamageMultiplier.GetValueAtLevel(level)}x");
            sb.Replace("{LEVEL}", $"{level}");
            return sb.ToString();
        }

        public (AlignmentDefinition, int) GetAlignmentLevel(int level) {
            return (alignment, level * 2); // hardcoded value, but easy to turn into parameter
        }

        protected abstract void ActivateActionImplementation(AbilityManager owner, AbilityInstance ability, ActionInstance action, Vector3 target, Action<AttributeSet> OnHit = null);

        protected void DelayedActivate(AbilityManager owner, AbilityInstance ability, ActionInstance action, Vector3 target, Action<AttributeSet> OnHit = null) {
            OnActivateEffects.ForEach(x => x.OnActivate(owner, ability, action, OnHit));
            ActivateActionImplementation(owner, ability, action, target, OnHit);
        }
    }
}