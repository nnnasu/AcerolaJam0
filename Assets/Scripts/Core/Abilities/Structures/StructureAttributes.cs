using System;
using Core.AttributeSystem;
using UnityEngine;

namespace Core.Abilities.Structures {
    public class StructureAttributes : MonoBehaviour, IDamageable {
        public float HP;
        public float MaxHP;

        public event Action<StructureAttributes> OnDeath = delegate { };
        public event Action<float, float> OnHPChanged = delegate { };

        public void TakeDamage(float amount) {
            float oldHP = HP;
            HP--;
            OnHPChanged?.Invoke(oldHP, HP);
            
            if (HP <= 0) {
                OnDeath?.Invoke(this);
            }
        }

        public void ResetState() {
            HP = MaxHP;
        }

        public EntityType GetEntityType() {
            return EntityType.Structure;
        }
    }
}