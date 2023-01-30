using LoxiGames.Manager;
using UnityEngine;

namespace LoxiGames
{
    public abstract class PoolObject : MonoBehaviour
    {
        public virtual void Activate()
        {
            gameObject.SetActive(true);
        }

        public virtual void Deactivate()
        {
            gameObject.SetActive(false);

            var tr = transform;
            tr.localPosition = Vector3.zero;
            tr.rotation = Quaternion.identity;
            tr.SetParent(ObjectPooler.Instance.transform);
        }
    }
}