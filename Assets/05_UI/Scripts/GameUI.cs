using System;
using DG.Tweening;
using LoxiGames.Manager;
using TMPro;
using UnityEngine;

namespace LoxiGames.UI
{
    public class GameUI : UI
    {
        [SerializeField] private TextMeshProUGUI levelCountText;
        [SerializeField] private TextMeshProUGUI coinCountText;
        [SerializeField] private RectTransform settingsHolder;

        private bool _settingsToggle;
        private Vector2 _settingsSizeDelta;

        private void OnEnable()
        {
            GameModeManager.OnGameModeChanged += OnGameModeChanged;
        }

        private void OnDisable()
        {
            GameModeManager.OnGameModeChanged -= OnGameModeChanged;
        }

        private void Start()
        {
            _settingsSizeDelta = settingsHolder.sizeDelta;
        }

        public void ToggleSettingsButtonClick()
        {
            var sizeDelta = _settingsSizeDelta;

            sizeDelta = _settingsToggle
                ? new Vector2(sizeDelta.x, sizeDelta.y)
                : new Vector2(sizeDelta.x, sizeDelta.y / 3f);

            _settingsToggle = !_settingsToggle;
            settingsHolder.DOSizeDelta(sizeDelta, .25f);
        }

        public void SetLevelCountText(string levelText)
        {
            levelCountText.text = levelText;
        }

        public void SetCoinText(string coinText)
        {
            coinCountText.text = coinText;
        }

        private void OnGameModeChanged(Type newMode)
        {
            levelCountText.transform.parent.gameObject.SetActive(newMode != typeof(ActionMode));
        }
    }
}