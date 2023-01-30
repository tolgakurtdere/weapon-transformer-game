using LoxiGames.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LoxiGames.Manager
{
    public class PrefsManager : MonoSingleton<PrefsManager>
    {
        [SerializeField, ReadOnly] [BoxGroup("Game")]
        private int levelIndex;

        private const string LevelIndexKey = "com.loxigames.levelindex";

        protected override void Awake()
        {
            base.Awake();
            levelIndex = PlayerPrefs.GetInt(LevelIndexKey);
        }

        /// <summary>
        /// In order to play specific level on the editor
        /// </summary>
        /// <param name="index"></param>
        [Button, DisableInPlayMode]
        private void SetLevelIndex(int index)
        {
            this.levelIndex = index;
            PlayerPrefs.SetInt(LevelIndexKey, this.levelIndex);
        }

        public int GetLevelIndex()
        {
            return levelIndex;
        }

        public void IncrementLevelIndex()
        {
            SetLevelIndex(levelIndex + 1);
        }
    }
}