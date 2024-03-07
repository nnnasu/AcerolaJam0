using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectIcon : PoolableBehaviour {
    public Image durationImage;
    public Image effectImage;

    Tween durationTween;

    public void Bind(EffectInstance effectInstance) {
        durationTween.Stop();
        if (effectInstance.effectDefinition.effectType == EffectType.Duration) {
            float duration = effectInstance.effectDefinition.duration.GetValueAtLevel(effectInstance.level);
            durationTween = Tween.UIFillAmount(durationImage, 1, 0, duration).OnComplete(Expire);
        }
        effectImage.sprite = effectInstance.effectDefinition.icon;
    }

    public void Expire() {
        durationTween.Stop();
        ReturnToPool();
    }


}