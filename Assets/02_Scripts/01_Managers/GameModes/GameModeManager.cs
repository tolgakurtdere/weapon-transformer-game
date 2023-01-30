using System;
using System.Collections.Generic;
using System.Linq;
using LoxiGames.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LoxiGames.Manager
{
    public class GameModeManager : MonoSingleton<GameModeManager>
    {
        public static event Action<Type> OnGameModeChanged;

        [ShowInInspector, ReadOnly] private List<GameModeBase> _gameModes;
        [ShowInInspector, ReadOnly] private GameModeBase _currentGameMode;

        private void OnEnable()
        {
            LevelManager.OnLevelLoaded += UpdateGameModes;
        }

        private void OnDisable()
        {
            LevelManager.OnLevelLoaded -= UpdateGameModes;
        }

        private void UpdateGameModes()
        {
            _gameModes = LevelManager.CurrentLevel.GameModes;
            //_gameModes.ForEach(mode => mode.Deactivate());

            _gameModes.ForEach(mode => { Debug.Log("Game Mode founded : " + mode.name, mode); });

            var defaultMode = _gameModes.Count > 1
                ? _gameModes.FirstOrDefault(mode => mode.IsDefault)
                : _gameModes.FirstOrDefault();

            if (!defaultMode)
            {
                Debug.Log("<color=red>There is not any default mode! Set one of them default!</color>");
                return;
            }

            if (defaultMode.GetType() == typeof(ActionMode)) UIManager.HomeUI.Hide();

            SetGameMode(defaultMode.GetType());
        }

        public void SetGameMode(Type gameModeType)
        {
            if (gameModeType == null)
            {
                throw new ArgumentException("Game Mode is null!");
            }

            if (!gameModeType.IsSubclassOf(typeof(GameModeBase)))
            {
                throw new ArgumentException($"{gameModeType} not inherits from GameModeBase");
            }

            var newMode = _gameModes.FirstOrDefault(gameMode => gameMode.GetType() == gameModeType);
            if (!newMode)
            {
                Debug.Log($"<color=red>{gameModeType} could not get found!</color>");
                return;
            }

            if (_currentGameMode) _currentGameMode.Deactivate();
            newMode.Activate();
            _currentGameMode = newMode;

            OnGameModeChanged?.Invoke(gameModeType);
            Debug.Log($"<color=green>New Game Mode: {gameModeType}</color>");
        }
    }
}