using UnityEngine;
namespace Core.AttributeSystem {

    [CreateAssetMenu(fileName = "BaseAttributes", menuName = "Templates/BaseAttributes", order = 0)]
    public class BaseAttributes : ScriptableObject {

        public float MaxHP;
        public float MovementSpeedBase;
        public float AttackSpeed;
        public float BaseAttack;

    }
}