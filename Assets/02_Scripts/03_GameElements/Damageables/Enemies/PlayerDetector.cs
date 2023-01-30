using System;
using LoxiGames.Manager;
using UnityEngine;

namespace WeaponTransformer.Damageable.Enemy
{
    [RequireComponent(typeof(Collider))]
    public class PlayerDetector : MonoBehaviour
    {
        public event Action<Transform> OnPlayerDetected;

        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                OnPlayerDetected?.Invoke(other.transform);
                Deactivate();
                HapticManager.Detected();
            }
        }

        private void Deactivate()
        {
            _collider.enabled = false;
        }
    }
}