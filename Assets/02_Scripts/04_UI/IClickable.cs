namespace LoxiGames.UI
{
    public interface IClickable
    {
        bool Interactable { get; set; }
        void OnClick();
    }
}