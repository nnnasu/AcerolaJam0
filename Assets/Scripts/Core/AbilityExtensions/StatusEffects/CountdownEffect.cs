using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DeathCount", menuName = "Ability System/Status Effects/Death Countdown", order = 0)]
public class CountdownEffect : StatusEffect {

    public override EffectInstance GetEffectInstance(AttributeSet target, int level) {
        return new CountdownEffectInstance(this, level);
    }

    public override void Apply(AttributeSet attributeSet, EffectInstance instance) {
        if (instance is CountdownEffectInstance cdInstance) {
            cdInstance.StartCountdown();
        }
    }

    public override string GetDescription(EffectInstance instance) {
        return "Will die once the timer runs out.";
    }

    public override void Remove(AttributeSet attributeSet, EffectInstance instance) {
        if (instance is CountdownEffectInstance cdInstance) {
            cdInstance.CancelCountdown();
        }
    }

}
