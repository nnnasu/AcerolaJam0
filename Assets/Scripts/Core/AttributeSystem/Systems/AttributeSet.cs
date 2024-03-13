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
    public bool IsInvulnerable = false;

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
        RemoveAllEffects();
        MaxHP = baseAttributes.MaxHP;
        if (resetHP) HP = MaxHP;
        MovementSpeed = baseAttributes.BaseMovementSpeed;
        AttackSpeed = baseAttributes.BaseAttackSpeed;
        BaseAttack = baseAttributes.Attack;
        DamageDealtMult = baseAttributes.DamageDealtMult;
        DamageTakenMult = baseAttributes.DamageTakenMult;
    }


    public virtual void TakeDamage(float amount) {
        if (IsInvulnerable) return;
        amount = Formulas.DamageTakenFormula(amount, DamageTakenMult);

        float oldHP = HP;
        HP -= amount;
        OnHPChanged?.Invoke(oldHP, HP);
        OnDamageTaken?.Invoke(amount);
        if (HP <= 0) OnDeath?.Invoke(this);
    }

    public virtual void Heal(float amount) {
        // amount = Formulas.DamageTakenFormula(amount, DamageTakenMult);
        // there is no heal% amp

        float oldHP = HP;
        HP += amount;
        OnHPChanged?.Invoke(oldHP, HP);
    }

    public void ApplyEffect(EffectInstance effect) {
        if (!effect.CanApplyEffect(this)) return;

        if (ActiveEffects.ContainsKey(effect.effectDefinition)) {
            RemoveEffect(effect, true); // just remove the existing effect and apply it again
        }
        effect.HandleEffectApplication(this);
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
        currentEffect.HandleEffectRemoval(this);
        ActiveEffects.Remove(effect.effectDefinition);

        if (!toReplace) {
            OnEffectRemoved?.Invoke(currentEffect);
        }
    }

    public void RemoveEffect(StatusEffect effect) {
        if (!ActiveEffects.ContainsKey(effect)) return;
        var currentEffect = ActiveEffects[effect];
        currentEffect.ExpiryTween.Stop();
        currentEffect.HandleEffectRemoval(this);
        ActiveEffects.Remove(effect);
        OnEffectRemoved?.Invoke(currentEffect);
    }

    public virtual void ApplyModifier(StatModifier modifier, int level, bool negate = false, int stacks = 1) {
        float rawValue = modifier.GetValue(level, stacks);
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

    public void RemoveAllEffects() {
        List<StatusEffect> effects = new();
        foreach (var item in ActiveEffects) {
            effects.Add(item.Key);
        }
        effects.ForEach(x => RemoveEffect(x));
    }

    public EntityType GetEntityType() {
        return entityType;
    }

    public void Kill() {
        TakeDamage(MaxHP);
    }

    public Transform GetTransform() {
        return transform;
    }
}
