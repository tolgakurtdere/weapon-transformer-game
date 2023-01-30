namespace LoxiGames.Manager
{
    public class FailureCam : VirtualCamera
    {
        protected override void Awake()
        {
            base.Awake();
            Type = CameraManager.CameraType.Failure;
        }
    }
}