using System.Collections;
using System.Collections.Generic;
using Core.Abilities.Instances;
using UnityEngine;

namespace Core.Abilities.Effects {

    // [CreateAssetMenu(fileName = "OnHitEffect", menuName = "Effects/OnHitEffect", order = 0)]
    public abstract class OnHitEffect : ScriptableObject {

        public abstract string GetTooltip(ActionInstance action);
        public abstract void OnHit(AbilityManager owner, AbilityInstance ability, ActionInstance action, AttributeSet target);

    }
}