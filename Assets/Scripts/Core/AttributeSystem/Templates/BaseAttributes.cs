using UnityEngine;
namespace Core.AttributeSystem {

    [CreateAssetMenu(fileName = "BaseAttributes", menuName = "Ability System/Templates/BaseAttributes", order = 0)]
    public class BaseAttributes : ScriptableObject {

        public float MaxHP;
        public float MovementSpeedBase;
        public float AttackSpeed;
        public float BaseAttack;

        [Range(0.001f, 10)]
        public float DamageDealtMult = 1;
        [Range(0.001f, 10)]
        public float DamageTakenMult = 1;


    }
}