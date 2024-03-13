using Core.AttributeSystem;
using UnityEngine;

public interface IDamageable {

    public void TakeDamage(float amount);
    public void Heal(float amount);
    public Transform GetTransform();

    public EntityType GetEntityType();
}