using System.Collections;
using System.Collections.Generic;
using Core.AbilityExtensions.Spawns;
using Core.Animation;
using Core.Enemies.Boss;
using Core.Enemies.Boss.Actions;
using Core.GlobalInfo;
using Core.Utilities.Scaling;
using UnityEngine;

namespace Core.Enemies.Boss.Actions {
    /// <summary>
    ///  Teleports to a random point in a circle around the player.
    /// </summary>
    [CreateAssetMenu(fileName = "Summon", menuName = "Boss/Teleport", order = 0)]
    public class BossTeleportAction : BossAction {

        public float CircleRadius = 7;
        public bool UsePlayerPosition = true;

        public override bool CanExecuteImpl(BossAIController boss) {
            return true;
        }

        public override void ExecuteImpl(BossAIController boss) {
            boss.CanMove = false;
            boss.CanTurn = false;
            Vector3 pos = UsePlayerPosition ? boss.cachedPlayerLocation : boss.transform.position;

            Vector3 offset = Random.insideUnitCircle;
            Vector3 newPos = pos + CircleRadius * new Vector3(offset.x, 0, offset.y);
            boss.Teleport(newPos);

        }
    }
}