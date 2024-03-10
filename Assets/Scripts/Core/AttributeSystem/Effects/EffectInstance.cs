using System;
using PrimeTween;

// probably generates garbage like crazy
public class EffectInstance {
    public StatusEffect effectDefinition;
    public int level;
    public Tween ExpiryTween;

    public EffectInstance(StatusEffect def, int level) {
        effectDefinition = def;
        this.level = level;
    }

    public virtual void HandleEffectApplication(AttributeSet attributes) {
        effectDefinition.Apply(attributes, this);
    }
    public virtual void HandleEffectRemoval(AttributeSet attributes) {
        effectDefinition.Remove(attributes, this);
    }

}