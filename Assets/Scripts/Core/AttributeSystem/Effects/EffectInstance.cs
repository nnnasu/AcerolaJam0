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

    public virtual void Apply(AttributeSet attributes) {
        effectDefinition.Apply(attributes, this);
    }
    public virtual void Remove(AttributeSet attributes) {
        effectDefinition.Remove(attributes, this);
    }

}