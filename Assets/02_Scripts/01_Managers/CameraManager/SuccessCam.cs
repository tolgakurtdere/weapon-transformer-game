namespace LoxiGames.Manager
{
    public class SuccessCam : VirtualCamera
    {
        protected override void Awake()
        {
            base.Awake();
            Type = CameraManager.CameraType.Success;
        }
    }
}