using LoxiGames.Manager;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace WeaponTransformer
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [SerializeField, Required] private WeaponData data;
        private float _lastFireTime;

        public WeaponData Data => data;
        private bool CanFire => Time.time >= _lastFireTime + 1 / data.FireRate;

        protected virtual void Initialize()
        {
            Debug.Log("Weapon Initialized! : " + name);
        }

        public virtual void Activate()
        {
            data.ButtonInfo.DisableButton();
            gameObject.SetActive(true);
        }

        public virtual void Deactivate()
        {
            data.ButtonInfo.EnableButton();
            gameObject.SetActive(false);
        }

        public void FireIfPossible(Vector3 targetPos)
        {
            if (!CanFire) return;

            //if fire is successful, update lastFireTime
            if (Fire(targetPos))
            {
                _lastFireTime = Time.time;
                HapticManager.Fire();
            }
        }

        protected abstract bool Fire(Vector3 targetPos);


        #region Development - DrawGizmos

#if UNITY_EDITOR
        protected virtual void OnDrawGizmosSelected()
        {
            if (!data) return;

            Handles.color = Color.red;
            var pos = transform.root.position;
            Handles.DrawWireDisc(pos, Vector3.up, data.Range);
            Handles.Label(pos + Vector3.forward * data.Range, name);
        }
#endif

        #endregion
    }
}