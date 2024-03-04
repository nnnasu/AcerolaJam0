using System;

namespace Core.AttributeSystem {
    [Flags]
    public enum EntityType {
        NONE = 0,
        Player = 1,
        Structure = 2,
        Enemy = 4,
        EnemyStructure = 8,
        Other = 16,
    }
}
