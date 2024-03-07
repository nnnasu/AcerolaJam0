using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Directors {

    [CreateAssetMenu(fileName = "EnemyWeights", menuName = "EnemyWeights", order = 0)]
    public class EnemyWeights : ScriptableObject {

        public List<EnemySpawnParameters> weights;
        
    }
}