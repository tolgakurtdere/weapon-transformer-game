namespace LoxiGames.Manager
{
    public class GameCam : VirtualCamera
    {
        protected override void Awake()
        {
            base.Awake();
            Type = CameraManager.CameraType.Game;
        }

        private void LateUpdate()
        {
            //SetFollowOffsetX(GameManager.PlayerLocalPosition.x);
        }
    }
}