using LoxiGames.Utility;

namespace LoxiGames.Manager
{
    public interface IGameMode : ILoxiActivate
    {
        bool IsDefault { get; }
    }
}