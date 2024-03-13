using System.Collections;
using System.Collections.Generic;
using Core.Abilities.Instances;
using UnityEngine;

namespace Core.Abilities.Effects {

    public abstract class OnHitEffect : ScriptableObject {

        public abstract string GetTooltip(int level);
        public abstract void OnHit(AbilityManager owner, AbilityInstance ability, ActionInstance action, IDamageable target);

    }
}