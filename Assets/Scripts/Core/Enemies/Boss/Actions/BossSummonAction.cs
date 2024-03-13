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
    [CreateAssetMenu(fileName = "Summon", menuName = "Boss/Summon", order = 0)]
    public class BossSummonAction : BossAction {

        public override bool CanExecuteImpl(BossAIController boss) {
            return true;
        }

        public override void ExecuteImpl(BossAIController boss) {
            boss.CanMove = false;
            boss.CanTurn = false;
            boss.OnSupportRequested?.Invoke();
        }
    }
}