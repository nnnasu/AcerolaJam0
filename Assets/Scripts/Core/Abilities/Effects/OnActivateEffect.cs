using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities.Instances;
using UnityEngine;

namespace Core.Abilities.Effects {

    public abstract class OnActivateEffect : ScriptableObject {

        public abstract string GetTooltip(ActionInstance action);
        public abstract void OnActivate(AbilityManager owner, AbilityInstance ability, ActionInstance action, Action<AttributeSet> OnHit = null);

    }
}