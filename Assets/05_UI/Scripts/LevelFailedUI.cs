using LoxiGames.Manager;

namespace LoxiGames.UI
{
    public class LevelFailedUI : UI
    {
        public void Restart()
        {
            TransitionManager.Instance.StartTransition(() => LevelManager.Instance.LoadLevel());
        }
    }
}