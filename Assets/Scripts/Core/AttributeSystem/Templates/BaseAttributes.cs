using Core.GlobalInfo;
using Core.Utilities.Scaling;
using UnityEngine;
namespace Core.AttributeSystem {

    [CreateAssetMenu(fileName = "BaseAttributes", menuName = "Ability System/Templates/BaseAttributes", order = 0)]
    public class BaseAttributes : ScriptableObject {

        [SerializeField] protected float BaseMaxHP;
        [SerializeField] protected float BaseAttack;
        public float BaseMovementSpeed;
        public float BaseAttackSpeed;

        public float MaxHP => MaxHPScaling.GetValueAtLevel(level) + BaseMaxHP;
        public float Attack => AttackScaling.GetValueAtLevel(level) + BaseAttack;

        [Range(0.001f, 10)]
        public float DamageDealtMult = 1;
        [Range(0.001f, 10)]
        public float DamageTakenMult = 1;

        [Header("Scaling Rules")]
        public ScaledFloat MaxHPScaling;
        public ScaledFloat AttackScaling;
        public int level => GameLevel.current == null ? 0 : GameLevel.current.level;

    }
}