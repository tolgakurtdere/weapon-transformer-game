using Sirenix.OdinInspector;
using UnityEngine;

namespace WeaponTransformer
{
    public abstract class MeleeWeaponBase : WeaponBase
    {
        [SerializeField, Min(0.1f)] private float damageRange = 1f;
        [SerializeField, Required] private Transform damagePoint;
        [SerializeField, Min(0)] private int damage = 1;

        protected override bool Fire(Vector3 targetPos)
        {
            var temps = Physics.OverlapSphere(damagePoint.position, damageRange, Data.TargetLayer);
            foreach (var temp in temps)
            {
                var damageable = temp.GetComponent<IDamageable>();
                damageable?.TakeDamage(damage);
            }

            return true;
        }


        #region Development - DrawGizmos

#if UNITY_EDITOR
        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(damagePoint.position, damageRange);
        }
#endif

        #endregion
    }
}