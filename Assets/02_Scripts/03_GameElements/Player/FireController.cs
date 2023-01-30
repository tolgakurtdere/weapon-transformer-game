using System.Collections.Generic;
using System.Linq;
using LoxiGames.Manager;
using Sirenix.OdinInspector;
using UnityEngine;
using WeaponTransformer.Collectable;

namespace WeaponTransformer.Player
{
    [RequireComponent(typeof(PlayerWrapper))]
    public class FireController : MonoBehaviour
    {
        [SerializeField] private LayerMask raycastLayerMask;
        [ShowInInspector, ReadOnly] private WeaponBase _equippedWeapon;
        [ShowInInspector, ReadOnly] private List<WeaponBase> _weapons;
        [ShowInInspector, ReadOnly] private readonly Collider[] _targetsInRange = new Collider[10];
        [ShowInInspector, ReadOnly] private Collider _target;
        private int _targetCount;
        private Animator _animator;
        private PlayerWrapper _playerWrapper;

        public bool IsFiring { get; private set; }

        private void OnEnable()
        {
            LevelManager.OnLevelStopped += OnLevelStopped;
            WeaponMenuController.OnWeaponSelected += OnWeaponSelected;
        }

        private void OnDisable()
        {
            LevelManager.OnLevelStopped -= OnLevelStopped;
            WeaponMenuController.OnWeaponSelected -= OnWeaponSelected;
        }

        private void Awake()
        {
            _weapons = GetComponentsInChildren<WeaponBase>(true).ToList();
            _playerWrapper = GetComponent<PlayerWrapper>();

            _animator = _playerWrapper.AnimatorController.Animator;
        }

        private void OnLevelStopped(bool isSuccess)
        {
            _target = null;
            _targetCount = 0;
            IsFiring = false;
            _animator.SetBool(AnimatorParameters.Bools.IsFiring, false);
        }

        private void Update()
        {
            if (!GameManager.IsPlaying) return;

            _target = null;
            _targetCount = Physics.OverlapSphereNonAlloc(transform.position, _equippedWeapon.Data.Range,
                _targetsInRange,
                _equippedWeapon.Data.TargetLayer);

            //find closest target
            var minDistance = float.PositiveInfinity;
            for (var i = 0; i < _targetCount; i++)
            {
                var sqrDistance = (transform.position - _targetsInRange[i].transform.position).sqrMagnitude;
                if (sqrDistance < minDistance)
                {
                    minDistance = sqrDistance;
                    _target = _targetsInRange[i];
                }
            }

            //check if there is an obstacle between player and target
            if (_target)
            {
                var dir = (_target.transform.position - transform.position).normalized;
                if (Physics.Raycast(_equippedWeapon.transform.position, dir, out var hit, _equippedWeapon.Data.Range,
                        raycastLayerMask))
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
                    {
                        _target = null;
                    }
                }
            }

            IsFiring = _target;
            _animator.SetBool(AnimatorParameters.Bools.IsFiring, IsFiring);
        }

        private void LateUpdate()
        {
            //if there is target, fire
            if (_target)
            {
                var pos = transform.position;
                var targetPos = _target.transform.position;
                targetPos.y = pos.y;
                transform.rotation = Quaternion.LookRotation(targetPos - pos);

                _equippedWeapon.FireIfPossible(targetPos);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out AmmoBox ammoBox))
            {
                if (_equippedWeapon is FirearmWeaponBase weapon)
                {
                    ammoBox.Collect();
                    weapon.Ammo += ammoBox.AmmoAmount;
                }
            }
        }

        private void OnWeaponSelected(WeaponData selectedWeapon)
        {
            var newWeapon = _weapons.FirstOrDefault(w => w.Data == selectedWeapon);
            if (!newWeapon)
            {
                Debug.LogError("WeaponData could not get found! : " + selectedWeapon);
                return;
            }

            if (_equippedWeapon)
            {
                _equippedWeapon.Deactivate();
                _animator.SetBool(_equippedWeapon.Data.AnimationParameter, false);
            }

            _equippedWeapon = newWeapon;
            _equippedWeapon.Activate();
            _animator.SetBool(_equippedWeapon.Data.AnimationParameter, true);
        }


        #region Development - DrawGizmos

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            for (var i = 0; i < _targetCount; i++)
            {
                Gizmos.DrawLine(transform.position, _targetsInRange[i].transform.position);
            }
        }
#endif

        #endregion
    }
}