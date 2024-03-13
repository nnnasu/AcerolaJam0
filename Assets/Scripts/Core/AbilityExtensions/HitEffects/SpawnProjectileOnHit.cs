using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using Core.Abilities.Effects;
using Core.Abilities.Instances;
using UnityEngine;

public class SpawnProjectileOnHit : OnHitEffect {
    public override string GetTooltip(int level) {
        return "";
    }

    public override void OnHit(AbilityManager owner, AbilityInstance ability, ActionInstance action, IDamageable target) {
        throw new System.NotImplementedException();
    }
}
