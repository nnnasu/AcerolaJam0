using System;
using System.Collections;
using System.Collections.Generic;
using Core.Abilities;
using Core.Abilities.Definitions;
using Core.Abilities.Instances;
using Core.AbilityExtensions.Spawns;
using Core.Animation;
using UnityEngine;

[CreateAssetMenu(fileName = "Melee Action", menuName = "Ability System/Player Actions/Melee", order = 0)]
public class MeleeAction : ActionDefinition {
    public GameObject HitboxPrefab;

    [Header("Animation Attachments")]
    public GameObject Attachment;
    public AttachmentSlot attachmentSlot = AttachmentSlot.RightHand;

    protected override void ActivateActionImplementation(AbilityManager owner, AbilityInstance ability, ActionInstance action, Vector3 target, Action<AttributeSet> OnHit = null) {
        var poolObj = GlobalPool.Current.GetObject(HitboxPrefab);
        AreaBurst obj = poolObj.GetComponent<AreaBurst>();
        if (obj == null) return;

        Vector3 spawnPoint = owner.transform.position + owner.transform.rotation * TargetOffset;
        obj.transform.position = spawnPoint;
        obj.transform.rotation = owner.transform.rotation;
        float damage = Formulas.DamageDealtFormula(
            owner.Attributes.BaseAttack,
            DamageMultiplier.GetValueAtLevel(action.level),
            owner.Attributes.DamageDealtMult
        );

        obj.Activate(damage, OnHit);
        obj.IgnoredEntities = IgnoredEntities;

        if (ability.StateToPlay.Equals(AnimationToPlay)) {

            var attach = GlobalPool.Current.GetObject(Attachment);
            var attachObj = attach.GetComponent<PoolableBehaviour>();
            float effectTime = UsageTime;
            if (actionType == Core.Abilities.Enums.ActionType.BasicAttack) effectTime = ability.cachedUsageTime;

            owner.AttachmentHandler.RightHand.ReplaceAttachment(attachObj);
            owner.AttachmentHandler.RightHand.ActivateEffects(true, effectTime);
        }

    }
}
