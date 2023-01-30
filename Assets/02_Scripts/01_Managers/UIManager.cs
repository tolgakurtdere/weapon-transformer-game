using LoxiGames.UI;
using LoxiGames.Utility;
using UnityEngine;

namespace LoxiGames.Manager
{
    public class UIManager : MonoSingleton<UIManager>
    {
        [SerializeField] private HomeUI homeUI;
        [SerializeField] private GameUI gameUI;
        [SerializeField] private LevelCompletedUI levelCompletedUI;
        [SerializeField] private LevelFailedUI levelFailedUI;

        public static HomeUI HomeUI => Instance.homeUI;
        public static GameUI GameUI => Instance.gameUI;
        public static LevelCompletedUI LevelCompletedUI => Instance.levelCompletedUI;
        public static LevelFailedUI LevelFailedUI => Instance.levelFailedUI;
    }
}