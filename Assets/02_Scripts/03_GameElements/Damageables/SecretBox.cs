using LoxiGames.Manager;
using Sirenix.OdinInspector;
using UnityEngine;

namespace WeaponTransformer.Damageable
{
    public class SecretBox : DamageableBase
    {
        [SerializeField, Min(0)] private int coinCount = 3;
        [SerializeField, Required] private GameObject obstacle;

        public override void Die()
        {
            base.Die();
            obstacle.SetActive(false);

            var childrenRigidbodies = GetComponentsInChildren<Rigidbody>();
            foreach (var rb in childrenRigidbodies)
            {
                rb.isKinematic = false;
                rb.AddExplosionForce(5, transform.position, 0.5f, 1);
            }

            for (var i = 0; i < coinCount; i++)
            {
                ObjectPooler.Instance.SpawnFromPool("coin", transform.position);
            }
        }
    }
}