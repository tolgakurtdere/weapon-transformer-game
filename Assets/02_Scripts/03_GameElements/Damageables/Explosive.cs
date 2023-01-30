using LoxiGames.Manager;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace WeaponTransformer.Damageable
{
    public class Explosive : DamageableBase
    {
        [SerializeField, Min(1)] private int damage = 1;
        [SerializeField, Min(0.5f)] private float explosionRadius = 1f;
        [SerializeField] private LayerMask targetLayer;
        [SerializeField, Required] private ParticleSystem explosionFx;
        [SerializeField, Required] private ParticleSystem burnFx;
        [SerializeField, Required] private ParticleSystem afterExplosionFx;
        [SerializeField, Required] private GameObject barrel;

        public override void Die()
        {
            base.Die();
            barrel.SetActive(false);

            var targets = Physics.OverlapSphere(transform.position, explosionRadius, targetLayer);
            foreach (var target in targets)
            {
                target.GetComponent<IDamageable>().TakeDamage(damage);
            }

            burnFx.Stop();
            explosionFx.Play();
            //afterExplosionFx.Play();

            CameraManager.Instance.ShakeCamera(0.5f, 0.5f, 0.3f);
            HapticManager.Explosion();
        }


        #region Development - DrawGizmos

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, Vector3.up, explosionRadius);
        }
#endif

        #endregion
    }
}