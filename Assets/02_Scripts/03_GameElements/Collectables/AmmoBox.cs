using LoxiGames.Manager;
using UnityEngine;

namespace WeaponTransformer.Collectable
{
    [RequireComponent(typeof(Collider))]
    public class AmmoBox : MonoBehaviour, ICollectable
    {
        [SerializeField, Min(1)] private int ammoAmount = 10;
        private Collider _collider;

        public int AmmoAmount => ammoAmount;

        public Collider Collider
        {
            get
            {
                if (!_collider) _collider = GetComponent<Collider>();
                return _collider;
            }
        }

        public void Collect()
        {
            gameObject.SetActive(false);
            HapticManager.Collect();
        }


        #region Development - DrawGizmos

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;

            var tr = transform;
            var localScale = tr.localScale;
            Gizmos.DrawWireSphere(tr.position + Vector3.up * localScale.x / 2, localScale.x / 2);
        }
#endif

        #endregion
    }
}