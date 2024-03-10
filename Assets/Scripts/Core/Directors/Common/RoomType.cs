using System.Collections.Generic;
using Core.Directors;
using Core.Utilities.Scaling;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.Directors.Common {

    [CreateAssetMenu(fileName = "RoomType", menuName = "Rooms/RoomType", order = 0)]
    public class RoomType : ScriptableObject {

        [Header("Scaling Formulas")]
        public ScalingFormula CreditFormula;
        public ScalingFormula SpawnFormula;
        public EnemyWeights SpawnList;
        public int SpawnAttempts = 5;

        [Header("Portal Effects")]
        public Color PortalColour = new(1, 1, 1, 1);
        public Color ParticleColour = new(1, 1, 1, 1);

        [Header("Valid Rooms")]
        public List<AssetReference> EligibleLevels = new();

        public AssetReference GetRandomLevel() {
            int index = Mathf.FloorToInt(Random.value * EligibleLevels.Count);
            return EligibleLevels[index];
        }

        public int GetSpawnCount(int level) {
            // TODO DERIVE
            return SpawnFormula.GetFlooredValue(level);
        }

        public float GetCredits(int level) {
            return CreditFormula.GetValue(level);
        }


    }
}