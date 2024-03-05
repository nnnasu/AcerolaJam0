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

    public event Action<EffectInstance> OnEffectApplied = delegate { };
    public event Action<EffectInstance> OnEffectRemoved = delegate { };

    public event Action<float> OnDamageTaken = delegate { };

    public bool ResetOnEnable = false;

    private void OnEnable() {
        if (ResetOnEnable) ResetState(true);
    }

    public virtual void ResetState(bool resetHP = false) {
        MaxHP = baseAttributes.MaxHP;
        if (resetHP) HP = MaxHP;
        MovementSpeed = baseAttributes.MovementSpeedBase;
        AttackSpeed = baseAttributes.AttackSpeed;
        BaseAttack = baseAttributes.BaseAttack;
    }


    public virtual void TakeDamage(float amount) {
        // Debug.Log(amount);
        float oldHP = HP;
        HP -= amount;
        OnHPChanged?.Invoke(oldHP, HP);
        OnDamageTaken?.Invoke(amount);
        if (HP <= 0) OnDeath?.Invoke(this);
    }

    public void ApplyEffect(EffectInstance effect) {
        if (ActiveEffects.ContainsKey(effect.effectDefinition)) {
            RemoveEffect(effect, true); // just remove the existing effect and apply it again
        }
        effect.effectDefinition.Apply(this, effect.level);
        ActiveEffects[effect.effectDefinition] = effect;
        if (effect.effectDefinition.effectType == EffectType.Duration) {
            effect.ExpiryTween = Tween.Delay(effect.effectDefinition.duration.GetValueAtLevel(effect.level), () => RemoveEffect(effect));
        }
        OnEffectApplied?.Invoke(effect);
    }

    public void RemoveEffect(EffectInstance effect, bool toReplace = false) {
        if (!ActiveEffects.ContainsKey(effect.effectDefinition)) return;
        var currentEffect = ActiveEffects[effect.effectDefinition];
        currentEffect.ExpiryTween.Stop();
        currentEffect.effectDefinition.Remove(this, currentEffect.level);

        if (!toReplace) {
            OnEffectRemoved?.Invoke(currentEffect);
        }
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
