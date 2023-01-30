using System;
using System.Collections.Generic;
using System.Linq;
using LoxiGames.Utility;
using MoreMountains.NiceVibrations;
using UnityEngine;

namespace LoxiGames.Manager
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        public static event Action OnLevelLoaded;
        public static event Action OnLevelStarted;
        public static event Action<bool> OnLevelStopped;

        [SerializeField] private bool startLevelAuto;
        [SerializeField] private List<Level> levelPrefabs = new List<Level>();

        public static Level CurrentLevel;
        public static Transform Thrash;

        private CyclingList<Level> _levels;

        public static int TotalLevelCount => Instance.levelPrefabs.Count;

        protected override void Awake()
        {
            base.Awake();

            //Initialize level list and loads the prefabs
            _levels = new CyclingList<Level>(levelPrefabs);

            //Create the thrash
            Thrash = new GameObject("Thrash").transform;
        }

        private void Start()
        {
            LoadLevel();
        }

        public void LoadLevel(bool nextLevel = false)
        {
            if (!levelPrefabs.Any()) return;
            if (nextLevel) PrefsManager.Instance.IncrementLevelIndex();

            ClearThrash();

            var levelToLoad = _levels.GetElement(PrefsManager.Instance.GetLevelIndex());
            CurrentLevel = Instantiate(levelToLoad, Vector3.zero, Quaternion.identity, Thrash);

            CameraManager.Instance.Init(CurrentLevel.CameraConfigs);

            UIManager.LevelCompletedUI.Hide();
            UIManager.LevelFailedUI.Hide();

            UIManager.GameUI.SetLevelCountText("Level " + (PrefsManager.Instance.GetLevelIndex() + 1));
            UIManager.HomeUI.Show(); //activate HomeUI at the beginning of the level

            if (startLevelAuto) StartLevel();

            OnLevelLoaded?.Invoke();
        }

        public static void StartLevel()
        {
            if (GameManager.IsPlaying) return;
            GameManager.IsPlaying = true;

            OnLevelStarted?.Invoke();
        }

        public static void StopLevel(bool isSuccess)
        {
            if (!GameManager.IsPlaying) return;
            GameManager.IsPlaying = false;

            if (isSuccess) //level succeed
            {
                MMVibrationManager.Haptic(HapticTypes.Success);
                CameraManager.Instance.SetCamera(CameraManager.CameraType.Success);
                UIManager.LevelCompletedUI.Show();
            }
            else //level failed
            {
                MMVibrationManager.Haptic(HapticTypes.Failure);
                CameraManager.Instance.SetCamera(CameraManager.CameraType.Failure);
                UIManager.LevelFailedUI.Show();
            }

            OnLevelStopped?.Invoke(isSuccess);
        }

        private static void ClearThrash()
        {
            ObjectPooler.Instance.ResetPools(); //to avoid destroy pool objects

            var count = Thrash.childCount;
            for (var i = count - 1; i >= 0; i--)
            {
                Destroy(Thrash.GetChild(i).gameObject);
            }
        }
    }
}