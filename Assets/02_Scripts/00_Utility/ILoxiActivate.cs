namespace LoxiGames.Utility
{
    public interface ILoxiActivate
    {
        bool IsActive { get; }
        void Activate();
        void Deactivate();
    }
}