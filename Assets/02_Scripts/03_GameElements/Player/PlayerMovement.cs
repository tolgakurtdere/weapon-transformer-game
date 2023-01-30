using System;
using DG.Tweening;
using LoxiGames.Manager;
using Sirenix.OdinInspector;
using UnityEngine;

namespace WeaponTransformer.Player
{
    [RequireComponent(typeof(PlayerWrapper))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField, Required] private float maxSpeed = 3;
        private Rigidbody _rigidbody;
        private Animator _animator;
        private bool _canMove = true;
        private PlayerWrapper _playerWrapper;
        private Joystick _joystick;

        private void OnEnable()
        {
            LevelManager.OnLevelLoaded += OnLevelLoaded;
            LevelManager.OnLevelStopped += OnLevelStopped;
            GameModeManager.OnGameModeChanged += OnGameModeChanged;
        }

        private void OnDisable()
        {
            LevelManager.OnLevelLoaded -= OnLevelLoaded;
            LevelManager.OnLevelStopped -= OnLevelStopped;
            GameModeManager.OnGameModeChanged -= OnGameModeChanged;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _playerWrapper = GetComponent<PlayerWrapper>();

            _animator = _playerWrapper.AnimatorController.Animator;
            _joystick = GameManager.Joystick;
        }

        private void Start()
        {
            WeaponMenuController.WeaponMenu.OnRadialMenuEnabled += OnWeaponMenuEnabled;
            WeaponMenuController.WeaponMenu.OnRadialMenuDisabled += OnWeaponMenuDisabled;
        }

        private void OnLevelLoaded()
        {
            var tr = transform;
            tr.DOKill();
            tr.position = Vector3.zero;
            tr.rotation = Quaternion.identity;
        }

        private void OnLevelStopped(bool isSuccess)
        {
            _joystick.OnPointerUp(null);
            _rigidbody.velocity = Vector3.zero;
            _animator.SetBool(AnimatorParameters.Bools.IsWalking, isSuccess);
        }

        private void OnGameModeChanged(Type newMode)
        {
            if (newMode == typeof(ActionMode))
            {
                OnLevelLoaded();
            }
        }

        private void OnWeaponMenuEnabled()
        {
            _canMove = false;
        }

        private void OnWeaponMenuDisabled()
        {
            _canMove = true;
        }

        private void FixedUpdate()
        {
            if (!GameManager.IsPlaying) return;
            if (!_canMove) return;

            var joystickDirection = _joystick.Direction;
            var direction = new Vector3(joystickDirection.x, 0, joystickDirection.y);
            _rigidbody.velocity = direction * maxSpeed;

            if (direction != Vector3.zero)
            {
                transform.DORotateQuaternion(Quaternion.LookRotation(direction), 0.2f);
                _animator.SetBool(AnimatorParameters.Bools.IsWalking, true);
                _animator.SetFloat(AnimatorParameters.Floats.WalkingSpeed,
                    Mathf.Max(maxSpeed / 10, maxSpeed / 5 * direction.magnitude));
            }
            else
            {
                _animator.SetBool(AnimatorParameters.Bools.IsWalking, false);
            }

            if (_playerWrapper.FireController.IsFiring) transform.DOKill();
        }
    }
}