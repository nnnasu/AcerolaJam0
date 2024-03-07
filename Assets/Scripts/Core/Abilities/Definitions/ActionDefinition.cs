using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities.Effects;
using Core.Abilities.Enums;
using Core.Abilities.Instances;
using Core.Utilities.Scaling;
using UnityEngine;



namespace Core.Abilities.Definitions {
    public abstract class ActionDefinition : ScriptableObject {
        [Header("Ability Parameters")]
        public ActionType actionType;
        public AlignmentType alignment;
        public ScaledFloat DamageMultiplier;
        public ScaledFloat BaseCooldown;
        public ScaledFloat BaseMPCost;
        public float UsageTime = 0;
        public List<OnHitEffect> OnHitEffects = new();
        public List<OnActivateEffect> OnActivateEffects = new();

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
        public virtual void ActivateAction(AbilityManager owner, AbilityInstance ability, ActionInstance action, Vector3 target, Action<AttributeSet> OnHit = null) {
            OnActivateEffects.ForEach(x => x.OnActivate(owner, ability, action, OnHit));
        }

        public virtual string GetTooltipText(float level) {
            return Description;
        }
    }
}