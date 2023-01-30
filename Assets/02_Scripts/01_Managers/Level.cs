using System.Collections.Generic;
using System.Linq;
using LoxiGames.Manager;
using Sirenix.OdinInspector;
using UnityEngine;
using WeaponTransformer;
using WeaponTransformer.Damageable.Enemy;

namespace LoxiGames
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private List<VirtualCameraConfigs> cameraConfigs;
        [ShowInInspector, ReadOnly] private FinishArea _finishArea;
        [ShowInInspector, ReadOnly] private List<GameModeBase> _gameModes;
        [ShowInInspector, ReadOnly] private List<EnemyBase> _enemies;
        [ShowInInspector, ReadOnly] private int _enemyCount;

        public List<VirtualCameraConfigs> CameraConfigs => cameraConfigs;
        public List<GameModeBase> GameModes => _gameModes;

        private void Awake()
        {
            _finishArea = GetComponentInChildren<FinishArea>();
            _gameModes = GetComponentsInChildren<GameModeBase>().ToList();
            _enemies = GetComponentsInChildren<EnemyBase>().ToList();
            _enemyCount = _enemies.Count;
        }

        private void OnEnable()
        {
            _enemies.ForEach(e => e.OnDied += OnEnemyDied);
        }

        private void OnDisable()
        {
            _enemies.ForEach(e => e.OnDied -= OnEnemyDied);
        }

        private void OnEnemyDied()
        {
            _enemyCount--;

            if (_enemyCount <= 0)
            {
                _finishArea.Activate();
            }
        }
    }
}