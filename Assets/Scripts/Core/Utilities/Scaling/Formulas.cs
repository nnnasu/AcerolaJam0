
using UnityEngine;

public static class Formulas {

    public static float BaseStructureTickInterval = 1;

    public static float AttackSpeedFormula(float baseAttackTime, float attackSpeed) {
        return Mathf.Clamp(
            baseAttackTime / (1 + attackSpeed / 30),
            0.2f,
            6
        );
    }

    public static float CooldownReductionFormula(float cooldown, float cdr) {
        return Mathf.Clamp((cooldown) / (1 + cdr / 100), 0, cooldown);
    }

    public static float StructureTickRateFormula(float StructureTickSpeed) {
        return Mathf.Clamp(
            BaseStructureTickInterval / (1 + StructureTickSpeed / 20),
            0.3f,
            BaseStructureTickInterval
        );
    }

    public static float DamageDealtFormula(float baseAttack, float actionMult, float modifierMult) {
        return baseAttack * actionMult * modifierMult;
    }

    public static float DamageTakenFormula(float incomingDamage, float damageTakenMult) {
        return incomingDamage * damageTakenMult;
    }

    public static int SpawnFormula(int level, int adjustment) {
        return level + adjustment;
    }

    public static float CreditFormula(int level, float extraLevels, float exponent) {
        float x = level + extraLevels;
        return Mathf.Pow(Mathf.Log(x), exponent);
    }

}