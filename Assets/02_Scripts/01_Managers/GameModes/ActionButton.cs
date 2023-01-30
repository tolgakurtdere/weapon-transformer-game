using LoxiGames.UI;

namespace LoxiGames.Manager
{
    public class ActionButton : ButtonBase
    {
        public override void OnClick()
        {
            base.OnClick();

            UIManager.HomeUI.Hide();
            TransitionManager.Instance.StartTransition(() => GameModeManager.Instance.SetGameMode(typeof(ActionMode)));
        }
    }
}