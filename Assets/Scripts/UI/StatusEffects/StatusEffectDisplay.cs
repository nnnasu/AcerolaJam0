using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectDisplay : MonoBehaviour {
    public Dictionary<StatusEffect, StatusEffectIcon> icons = new();
    public GameObject iconTemplate;
    public HoverTipManager HoverTipManager;



    public void ApplyEffect(EffectInstance instance) {
        if (!instance.effectDefinition.ShowInHUD) return;
        if (icons.ContainsKey(instance.effectDefinition)) {
            var existing = icons[instance.effectDefinition];
            existing.Bind(instance);
            RegisterIconEvents(existing);
            return;
        }

        var obj = GlobalPool.Current.GetObject(iconTemplate);
        var icon = obj.GetComponent<StatusEffectIcon>();
        if (!icon) return;

        RegisterIconEvents(icon);

        icon.transform.SetParent(transform);
        icon.gameObject.SetActive(true);
        icons.Add(instance.effectDefinition, icon);
        icon.Bind(instance);
    }

    private void RegisterIconEvents(StatusEffectIcon icon) {

        icon.OnHoverEvent -= DisplayTip;
        icon.OnHoverLeft -= HideTip;
        icon.OnExpiryEvent -= OnIconReturn;
        icon.OnHoverEvent += DisplayTip;
        icon.OnHoverLeft += HideTip;
        icon.OnExpiryEvent += OnIconReturn;
    }
    public void RemoveEffect(EffectInstance instance) {
        if (!icons.ContainsKey(instance.effectDefinition)) return;
        if (!instance.effectDefinition.ShowInHUD) return;
        icons[instance.effectDefinition].Expire();
        icons.Remove(instance.effectDefinition);
    }

    private void DisplayTip(EffectInstance effect, Vector2 pos) {
        var tip = effect.effectDefinition.GetDescription(effect);
        HoverTipManager.ShowTip("", tip, pos);

    }

    private void HideTip() {
        HoverTipManager.HideTip();
    }

    private void OnIconReturn(StatusEffectIcon icon) {
        icon.OnHoverEvent -= DisplayTip;
        icon.OnHoverLeft -= HideTip;
        icon.OnExpiryEvent -= OnIconReturn;
    }
}
