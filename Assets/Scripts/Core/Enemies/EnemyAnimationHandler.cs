
using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour {
    public Animator animator;
    private static int IsMovingHash = Animator.StringToHash("IsMoving");
    private static int WalkSpeedMultHash = Animator.StringToHash("WalkSpeedMult");
    private static int AttackSpeedMultHash = Animator.StringToHash("AttackSpeedMult");
    private static int AttackTriggerHash = Animator.StringToHash("AttackTrigger");
    private static int DeathTriggerHash = Animator.StringToHash("DeathTrigger");

    public bool IsWalking = false;
    public float deathAnimationTimer = 1;

    public void SetMovement(bool isMoving, float multiplier = 1) {
        animator.SetFloat(WalkSpeedMultHash, multiplier);
        animator.SetBool(IsMovingHash, isMoving);
        IsWalking = isMoving;
    }

    public void SetAttackTrigger(float multiplier = 1) {
        animator.SetTrigger(AttackTriggerHash);
        animator.SetFloat(AttackSpeedMultHash, multiplier);
    }

    public float SetDeath() {
        animator.SetTrigger(DeathTriggerHash);
        return deathAnimationTimer;
    }


}