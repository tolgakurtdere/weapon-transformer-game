using System;
using System.Collections;
using DG.Tweening;
using LoxiGames.Manager;
using LoxiGames.Utility;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using WeaponTransformer.Player;
using Random = UnityEngine.Random;

namespace WeaponTransformer.Damageable.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class EnemyBase : DamageableBase
    {
        public event Action OnDied;

        [SerializeField, Min(1)] private int damageAmount = 10;
        [SerializeField, Min(0)] private int coinCount = 5;
        [SerializeField, Min(0)] private float walkSpeed = 1f;
        [SerializeField, Min(0)] private float runSpeed = 5.5f;
        [SerializeField] private Color deadColor;
        [ShowInInspector, ReadOnly] private Transform _detectedPlayer;

        private NavMeshAgent _navMeshAgent;
        private HealthBar _healthBar;
        private int _health;
        private PlayerDetector _playerDetector;
        private Animator _animator;
        private Renderer _renderer;
        private Vector3 _initPosition;

        private Coroutine _patrolRoutine;
        private Coroutine _catchPlayerRoutine;
        private Coroutine _randomWalkRoutine;

        public int DamageAmount => damageAmount;

        public override int Health
        {
            get => _health;
            protected set
            {
                _health = value;
                _healthBar.Set(Health, initialHealth);
            }
        }

        private void OnEnable()
        {
            PlayerHealthController.OnPlayerDied += OnPlayerDied;
            _playerDetector.OnPlayerDetected += OnPlayerDetected;
        }

        private void OnDisable()
        {
            PlayerHealthController.OnPlayerDied -= OnPlayerDied;
            _playerDetector.OnPlayerDetected -= OnPlayerDetected;
        }

        protected override void Awake()
        {
            base.Awake();

            _navMeshAgent = GetComponent<NavMeshAgent>();
            _healthBar = GetComponentInChildren<HealthBar>();
            _playerDetector = GetComponentInParent<PlayerDetector>();
            _animator = GetComponentInChildren<Animator>();
            _renderer = GetComponentInChildren<Renderer>();
        }

        protected override void Start()
        {
            base.Start();

            _initPosition = transform.position;
            _patrolRoutine = StartCoroutine(Patrol());
        }

        private void OnPlayerDied()
        {
            if (!_navMeshAgent.enabled) return;

            if (_catchPlayerRoutine != null)
            {
                StopCoroutine(_catchPlayerRoutine);
                _randomWalkRoutine = StartCoroutine(RandomWalk());
            }

            _detectedPlayer = null;
        }

        private void OnPlayerDetected(Transform player)
        {
            if (!_navMeshAgent.enabled) return;

            _detectedPlayer = player;

            if (_patrolRoutine != null) StopCoroutine(_patrolRoutine);
            _catchPlayerRoutine = StartCoroutine(CatchPlayer());
        }

        public override void Die()
        {
            base.Die();

            _healthBar.Deactivate();
            _navMeshAgent.enabled = false;

            _animator.SetTrigger(AnimatorParameters.Triggers.Die);
            _renderer.material.DOColor(deadColor, 0.5f);

            for (var i = 0; i < coinCount; i++)
            {
                ObjectPooler.Instance.SpawnFromPool("coin", transform.position);
            }

            StopAllCoroutines();
            OnDied?.Invoke();
            HapticManager.Kill();
        }

        private IEnumerator Patrol()
        {
            _navMeshAgent.speed = walkSpeed;
            _animator.SetTrigger(AnimatorParameters.Triggers.Walk);

            while (!_detectedPlayer)
            {
                var r = Random.insideUnitSphere * 4;
                r.y = 0;
                _navMeshAgent.SetDestination(_initPosition + r);

                yield return new WaitUntil(
                    () => _navMeshAgent.remainingDistance < _navMeshAgent.stoppingDistance + 0.1f);
            }
        }

        private IEnumerator CatchPlayer()
        {
            _navMeshAgent.speed = runSpeed;
            _animator.SetTrigger(AnimatorParameters.Triggers.Run);

            while (_detectedPlayer)
            {
                _navMeshAgent.SetDestination(_detectedPlayer.position);

                yield return null;
            }
        }

        private IEnumerator RandomWalk()
        {
            _navMeshAgent.speed = walkSpeed;
            _animator.SetTrigger(AnimatorParameters.Triggers.Walk);

            while (true)
            {
                var r = Random.insideUnitSphere * 10;
                r.y = 0;
                _navMeshAgent.SetDestination(transform.position + r);

                yield return new WaitUntil(
                    () => _navMeshAgent.remainingDistance < _navMeshAgent.stoppingDistance + 0.1f);
            }
        }
    }
}