
using Core.GlobalInfo;
using Core.Utilities.Scaling;
using UnityEngine;

public class AttributeScaler : MonoBehaviour {

    public AttributeSet attributes;
    public ScaledFloat MaxHPScaling;
    public ScaledFloat BaseAttackScaling;
    public int level => GameLevel.current == null ? 0 : GameLevel.current.level;

    [ContextMenu("Apply Scaling")]
    public void AddScaledStats() {
        float hp = MaxHPScaling.GetValueAtLevel(level);
        attributes.MaxHP += hp;
        attributes.HP += hp;
        float attack = BaseAttackScaling.GetValueAtLevel(level);
        attributes.BaseAttack += attack;
    }

    private void OnEnable() {
        attributes.ResetState(true);
        AddScaledStats();
    }



}