using LoxiGames.Settings;
using LoxiGames.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LoxiGames.Manager
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField, Required] private GameSettings gameSettings;
        [SerializeField, Required] private PlayerSettings playerSettings;
        [SerializeField, Required] private Joystick joystick;

        public static bool IsPlaying { get; set; }
        public static GameSettings GameSettings => Instance.gameSettings;
        public static PlayerSettings PlayerSettings => Instance.playerSettings;
        public static Joystick Joystick => Instance.joystick;

        protected override void Awake()
        {
            base.Awake();
            Application.targetFrameRate = 60;
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) Time.timeScale = 0;
            if (Input.GetKeyDown(KeyCode.S)) Time.timeScale = 1;
        }
#endif
    }
}