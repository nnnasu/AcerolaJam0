using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.AbilityExtensions.StatusEffects.Stackables {
    public class StackableEffectInstance : EffectInstance {

        public int stackCount = 0;
        public StackableEffectInstance(StatusEffect def, int level, int stacks) : base(def, level) {
            stackCount = stacks;

        }




    }
}