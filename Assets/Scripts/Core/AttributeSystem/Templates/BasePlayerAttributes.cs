using UnityEngine;
namespace Core.AttributeSystem {

    [CreateAssetMenu(fileName = "PlayerAttributes", menuName = "Templates/PlayerAttributes", order = 0)]
    public class BasePlayerAttributes : BaseAttributes {

        public float MaxMP;
        public float MPRegenPercent;
        public float HPRegenPercent;
        public float StructureTickSpeed;
        public float CooldownReduction;

    }
}