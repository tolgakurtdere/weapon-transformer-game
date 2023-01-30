using UnityEngine;

namespace WeaponTransformer.Shop
{
    [RequireComponent(typeof(Collider))]
    public class Shop : MonoBehaviour
    {
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ShopManager.Instance.Activate();
            }
        }
    }
}