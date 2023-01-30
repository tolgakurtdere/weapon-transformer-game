using System.Collections;
using LoxiGames;
using UnityEngine;

namespace WeaponTransformer
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : PoolObject
    {
        [SerializeField, Min(0)] private int damage = 1;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damage);
                Deactivate();
            }
        }

        public void Fire(Vector3 direction)
        {
            transform.rotation = Quaternion.LookRotation(direction);
            _rigidbody.AddForce(direction * 1000);
            Deactivate(0.7f);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            _rigidbody.velocity = Vector3.zero;
        }

        private void Deactivate(float delay)
        {
            if (delay > 0)
            {
                StartCoroutine(DeactivateWithDelay(delay));
            }
            else
            {
                Deactivate();
            }
        }

        private IEnumerator DeactivateWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            Deactivate();
        }
    }
}