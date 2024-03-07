using System;
using System.Collections;
using System.Collections.Generic;
using Core.AttributeSystem;
using PrimeTween;
using UnityEngine;

public class AttributeSet : MonoBehaviour, IDamageable {

    public BaseAttributes baseAttributes;

    public float HP;
    public float MaxHP;
    public float MovementSpeed;
    public float AttackSpeed;
    public float BaseAttack;
    public float DamageDealtMult = 1;
    public float DamageTakenMult = 1;

    public EntityType entityType = EntityType.Enemy;

    public event Action<AttributeSet> OnDeath = delegate { };
    public event Action<float, float> OnHPChanged = delegate { };

    public Dictionary<StatusEffect, EffectInstance> ActiveEffects = new();

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
        DamageDealtMult = baseAttributes.DamageDealtMult;
        DamageTakenMult = baseAttributes.DamageTakenMult;
    }


    public virtual void TakeDamage(float amount) {
        amount = Formulas.DamageTakenFormula(amount, DamageTakenMult);

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
        effect.Apply(this);
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
        currentEffect.Remove(this);

        if (!toReplace) {
            OnEffectRemoved?.Invoke(currentEffect);
        }
    }

    public virtual void ApplyModifier(StatModifier modifier, int level, bool negate = false, float mult = 1) {
        float rawValue = modifier.value.GetValueAtLevel(level) * (mult != 0 ? mult : 1);
        float value = rawValue;
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
            case GameAttributes.DamageTaken:
                if (negate) DamageTakenMult /= rawValue;
                else DamageTakenMult *= rawValue;
                break;
            case GameAttributes.DamageDealt:
                if (negate) DamageDealtMult /= rawValue;
                else DamageDealtMult *= rawValue;
                break;
            default: break;
        }
    }


}
