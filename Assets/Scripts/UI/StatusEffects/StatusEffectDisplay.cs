using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectDisplay : MonoBehaviour {
    public Dictionary<StatusEffect, StatusEffectIcon> icons = new();
    public GameObject iconTemplate;

    public void ApplyEffect(EffectInstance instance) {
        if (!instance.effectDefinition.ShowInHUD) return;
        if (icons.ContainsKey(instance.effectDefinition)) {
            icons[instance.effectDefinition].Bind(instance);
            return;
        }

        var obj = GlobalPool.Current.GetObject(iconTemplate);
        var icon = obj.GetComponent<StatusEffectIcon>();
        if (!icon) return;
        icon.transform.SetParent(transform);
        icon.gameObject.SetActive(true);
        icons.Add(instance.effectDefinition, icon);
        icon.Bind(instance);
    }

    public void RemoveEffect(EffectInstance instance) {
        if (!icons.ContainsKey(instance.effectDefinition)) return;
        if (!instance.effectDefinition.ShowInHUD) return;
        icons[instance.effectDefinition].Expire();
        icons.Remove(instance.effectDefinition);
    }
}
