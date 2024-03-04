using System;
using PrimeTween;

public class EffectInstance {
    public GameplayEffect effectDefinition;
    public int level;

    public Tween ExpiryTween;

    public EffectInstance(GameplayEffect def, int level) {
        effectDefinition = def;
        this.level = level;
    }

}