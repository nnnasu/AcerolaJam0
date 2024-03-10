using Core.AttributeSystem;

public interface IDamageable {

    public void TakeDamage(float amount);

    public EntityType GetEntityType();
}