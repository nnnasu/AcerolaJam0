using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatusEffectIcon : PoolableBehaviour, IPointerMoveHandler, IPointerEnterHandler, IPointerExitHandler {
    public Image durationImage;
    public Image effectImage;
    Tween durationTween;
    private EffectInstance effect;


    public event Action<EffectInstance, Vector2> OnHoverEvent = delegate { };
    public event Action OnHoverLeft = delegate { };
    public event Action<StatusEffectIcon> OnExpiryEvent = delegate { };

    public void Bind(EffectInstance effectInstance) {
        effect = effectInstance;
        durationTween.Stop();
        if (effectInstance.effectDefinition.effectType == EffectType.Duration) {
            float duration = effectInstance.effectDefinition.duration.GetValueAtLevel(effectInstance.level);
            durationTween = Tween.UIFillAmount(durationImage, 1, 0, duration).OnComplete(Expire);
        }
        effectImage.sprite = effectInstance.effectDefinition.icon;
    }

    public void Expire() {
        OnExpiryEvent?.Invoke(this);
        durationTween.Stop();
        ReturnToPool();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        OnHoverEvent?.Invoke(effect, eventData.position);
    }

    public void OnPointerExit(PointerEventData eventData) {
        OnHoverLeft?.Invoke();
    }

    void IPointerMoveHandler.OnPointerMove(PointerEventData eventData) {
        OnHoverEvent?.Invoke(effect, eventData.position);
    }
}