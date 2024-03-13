using System.Collections;
using System.Collections.Generic;
using Core.Animation;
using PrimeTween;
using UnityEngine;

namespace Core.Enemies.Boss.Actions {

    // [CreateAssetMenu(fileName = "BossAction", menuName = "Boss/Action", order = 0)]
    public abstract class BossAction : ScriptableObject {

        public float CooldownTime;
        public float IdleTime; // delay the next action by this amount
        public float MaximumRange;

        [Range(0, 1)]
        public float CastPoint = 0;
        public AnimationStateInfo animation;


        public bool CanExecute(BossAIController boss) {
            if (boss.Cooldowns.Contains(this)) return false;
            if (Vector3.Distance(boss.cachedPlayerLocation, boss.transform.position) > MaximumRange) return false;
            return CanExecuteImpl(boss);
        }
        public abstract bool CanExecuteImpl(BossAIController boss);

        public float Execute(BossAIController boss) {
            if (CooldownTime > 0) {
                boss.Cooldowns.Add(this);
                Tween.Delay(CooldownTime, () => boss.Cooldowns.Remove(this));
            }
            boss.state = BossStates.Acting;
            boss.animationHandler.PlayAnimationState(animation);
            Tween.Delay(CastPoint * animation.UsageTime, () => ExecuteImpl(boss));
            return animation.UsageTime + IdleTime;
        }

        public abstract void ExecuteImpl(BossAIController boss);

    }
}