using Sirenix.OdinInspector;
using UnityEngine;

namespace WeaponTransformer.Damageable
{
    [RequireComponent(typeof(Collider))]
    public abstract class DamageableBase : MonoBehaviour, IDamageable
    {
        [SerializeField, Min(1)] protected int initialHealth = 1;
        private Collider _collider;

        public virtual int Health { get; protected set; }

        protected virtual void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        protected virtual void Start()
        {
            Health = initialHealth;
        }

        [Button]
        public virtual void TakeDamage(int damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                Die();
            }
        }

        [Button]
        public virtual void Die()
        {
            _collider.enabled = false;
        }
    }
}