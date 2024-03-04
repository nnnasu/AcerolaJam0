using System;
using System.Collections;
using System.Collections.Generic;
using Core.AttributeSystem;
using PrimeTween;
using UnityEngine;

public class AttributeSet : MonoBehaviour {

    public BaseAttributes baseAttributes;

    public float HP;
    public float MaxHP;
    public float MovementSpeed;
    public float AttackSpeed;
    public float BaseAttack;
    public EntityType entityType = EntityType.Enemy;

    public event Action<AttributeSet> OnDeath = delegate { };
    public event Action<float, float> OnHPChanged = delegate { };

    public Dictionary<GameplayEffect, EffectInstance> ActiveEffects = new();


    public virtual void ResetState(bool resetHP = false) {
        MaxHP = baseAttributes.MaxHP;
        if (resetHP) HP = MaxHP;
        MovementSpeed = baseAttributes.MovementSpeedBase;
        AttackSpeed = baseAttributes.AttackSpeed;
        BaseAttack = baseAttributes.BaseAttack;

    }

    private void OnEnable() {
        ResetState(true);
    }

    public virtual void TakeDamage(float amount) {
        float oldHP = HP;
        HP -= amount;
        OnHPChanged?.Invoke(oldHP, HP);
        if (HP <= 0) OnDeath?.Invoke(this);
    }

    public void ApplyEffect(EffectInstance effect) {
        if (ActiveEffects.ContainsKey(effect.effectDefinition)) {
            RemoveEffect(effect); // just remove the existing effect and apply it again
        }
        effect.effectDefinition.Apply(this, effect.level);
        ActiveEffects[effect.effectDefinition] = effect;
        if (effect.effectDefinition.effectType == EffectType.Duration) {
            effect.ExpiryTween = Tween.Delay(effect.effectDefinition.duration.GetValueAtLevel(effect.level), () => RemoveEffect(effect));
        }
    }

    public void RemoveEffect(EffectInstance effect) {
        if (!ActiveEffects.ContainsKey(effect.effectDefinition)) return;
        var currentEffect = ActiveEffects[effect.effectDefinition];
        currentEffect.ExpiryTween.Stop();
        currentEffect.effectDefinition.Remove(this, currentEffect.level);
    }

    public virtual void ApplyModifier(StatModifier modifier, int level, bool negate = false) {
        float value = modifier.value.GetValueAtLevel(level);
        if (negate) value *= -1;

        switch (modifier.Attribute) {
            case GameAttributes.MaxHP:
                MaxHP += value;
                break;
            case GameAttributes.MovementSpeed:
                MovementSpeed += value;
                break;
            case GameAttributes.AttackSpeed:
                AttackSpeed += value;
                break;
            case GameAttributes.BaseAttack:
                BaseAttack += value;
                break;
            default: break;
        }


    }


}
