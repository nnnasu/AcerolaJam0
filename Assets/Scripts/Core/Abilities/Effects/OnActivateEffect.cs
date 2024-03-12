using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Abilities.Instances;
using Core.AttributeSystem.Conditions;
using UnityEngine;

namespace Core.Abilities.Effects {

    public abstract class OnActivateEffect : ScriptableObject {
        public List<TargetCondition> UserConditions = new();

        public abstract string GetTooltip(int level);

        public bool CanActivate(AbilityManager owner, AbilityInstance ability, ActionInstance action) {
            if (UserConditions.Count == 0) return true;
            return UserConditions.All(x => x.TestCondition(owner.Attributes));
        }

        public abstract void OnActivateImpl(AbilityManager owner, AbilityInstance ability, ActionInstance action, Action<AttributeSet> OnHit = null);

        public void OnActivate(AbilityManager owner, AbilityInstance ability, ActionInstance action, Action<AttributeSet> OnHit = null) {
            if (!CanActivate(owner, ability, action)) return;
            OnActivateImpl(owner, ability, action, OnHit);
        }

    }
}