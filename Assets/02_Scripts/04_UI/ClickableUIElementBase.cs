namespace LoxiGames.UI
{
    public abstract class ClickableUIElementBase : UIElementBase, IClickable
    {
        public abstract bool Interactable { get; set; }
        public abstract void OnClick();
    }
}