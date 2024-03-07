using System;
using PrimeTween;


public class EffectInstance {
    public StatusEffect effectDefinition;
    public int level;
    public Tween ExpiryTween;

    public EffectInstance(StatusEffect def, int level) {
        effectDefinition = def;
        this.level = level;
    }

    public virtual void Apply(AttributeSet attribute) {
        
    }

    public virtual void Remove(AttributeSet attribute) {

    }

}