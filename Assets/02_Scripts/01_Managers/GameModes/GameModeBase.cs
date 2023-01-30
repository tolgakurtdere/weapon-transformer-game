using UnityEngine;

namespace LoxiGames.Manager
{
    public abstract class GameModeBase : MonoBehaviour, IGameMode
    {
        public abstract bool IsDefault { get; }
        public bool IsActive { get; private set; }

        private void Awake()
        {
            Deactivate();
        }

        public void Activate()
        {
            IsActive = true;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            IsActive = false;
            gameObject.SetActive(false);
        }
    }
}