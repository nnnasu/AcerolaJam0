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
    [CreateAssetMenu(fileName = "AOE", menuName = "Boss/AOE", order = 0)]
    public class BossAOEAction : BossAction {

        public GameObject aoe;
        public float duration = 1f;
        public ScaledFloat damageMult;
        public Vector3 offset = Vector3.forward;

        public override bool CanExecuteImpl(BossAIController boss) {
            return true;
        }

        public override void ExecuteImpl(BossAIController boss) {
            boss.CanMove = false;
            boss.CanTurn = false;
            var obj = GlobalPool.Current.GetObject(aoe);
            var area = obj.GetComponent<AreaBurst>();

            float damage = Formulas.DamageDealtFormula(boss.attributes.BaseAttack,
                damageMult.GetValueAtLevel(GameLevel.current.level),
                boss.attributes.DamageDealtMult);

            area.linger = duration;
            area.Activate(damage);
            area.IgnoredEntities = AttributeSystem.EntityType.Enemy;

            area.transform.position = boss.transform.position + boss.transform.rotation * offset;
        }
    }
}