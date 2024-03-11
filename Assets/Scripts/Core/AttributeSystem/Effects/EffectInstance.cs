using System;
using PrimeTween;

// probably generates garbage like crazy
public class EffectInstance {
    public StatusEffect effectDefinition;
    public int level;
    public Tween ExpiryTween;
    protected AttributeSet target;

    public EffectInstance(StatusEffect def, int level) {
        effectDefinition = def;
        this.level = level;
    }

    public bool CanApplyEffect(AttributeSet attributes) {
        return effectDefinition.CanApplyEffect(attributes, this);
    }

    public virtual void HandleEffectApplication(AttributeSet attributes) {
        target = attributes;
        effectDefinition.Apply(attributes, this);
    }
    public virtual void HandleEffectRemoval(AttributeSet attributes) {
        target = attributes;
        effectDefinition.Remove(attributes, this);
    }

}