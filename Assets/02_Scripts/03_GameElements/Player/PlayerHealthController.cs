using System;
using System.Collections;
using LoxiGames.Manager;
using LoxiGames.Utility;
using Sirenix.OdinInspector;
using UnityEngine;
using WeaponTransformer.Collectable;
using WeaponTransformer.Damageable.Enemy;

namespace WeaponTransformer.Player
{
    [RequireComponent(typeof(PlayerWrapper))]
    [RequireComponent(typeof(Collider))]
    public class PlayerHealthController : MonoBehaviour
    {
        public static event Action OnPlayerDied;

        [SerializeField, Min(1)] protected int initialHealth = 100;
        private HealthBar _healthBar;
        private int _health;
        private Collider _collider;
        private bool _canTakeDamage = true;

        private int Health
        {
            get => _health;
            set
            {
                if (value > initialHealth) value = initialHealth;
                _health = value;
                _healthBar.Set(Health, initialHealth);
            }
        }

        private void OnEnable()
        {
            LevelManager.OnLevelLoaded += OnLevelLoaded;
        }

        private void OnDisable()
        {
            LevelManager.OnLevelLoaded -= OnLevelLoaded;
        }

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _healthBar = GetComponentInChildren<HealthBar>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out HealBox healBox))
            {
                healBox.Collect();
                Heal(healBox.HealAmount);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out EnemyBase enemy))
            {
                TakeDamage(enemy.DamageAmount);
            }
        }

        private void OnLevelLoaded()
        {
            _healthBar.Activate();
            Health = initialHealth;
            _collider.enabled = true;
        }

        [Button]
        private void TakeDamage(int damage)
        {
            if (!_canTakeDamage) return;

            StartCoroutine(Undamaged());
            Health -= damage;

            if (Health <= 0)
            {
                Die();
            }

            HapticManager.TakeDamage();
        }

        [Button]
        private void Heal(int amount)
        {
            Health += amount;
        }

        [Button]
        private void Die()
        {
            _healthBar.Deactivate();
            _collider.enabled = false;

            LevelManager.StopLevel(false);

            OnPlayerDied?.Invoke();
        }

        private IEnumerator Undamaged()
        {
            _canTakeDamage = false;

            yield return new WaitForSeconds(1f);

            _canTakeDamage = true;
        }
    }
}