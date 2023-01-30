using LoxiGames.Utility;
using UnityEngine;

namespace LoxiGames.Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Loxi Games/Settings/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private FXPlayer confetti;

        public FXPlayer Confetti => confetti;
    }
}