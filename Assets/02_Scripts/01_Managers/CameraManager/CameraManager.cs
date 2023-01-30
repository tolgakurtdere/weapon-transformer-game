using System.Collections.Generic;
using System.Linq;
using LoxiGames.Utility;
using Sirenix.OdinInspector;

namespace LoxiGames.Manager
{
    public class CameraManager : MonoSingleton<CameraManager>
    {
        public enum CameraType
        {
            Game,
            Success,
            Failure
        }

        private List<VirtualCamera> _virtualCameras;
        [ShowInInspector, ReadOnly] private VirtualCamera _currentCamera;

        protected override void Awake()
        {
            base.Awake();

            _virtualCameras = GetComponentsInChildren<VirtualCamera>().ToList();
        }

        /// <summary>
        /// Set camera configs (follow, lookAt)
        /// </summary>
        public void Init(List<VirtualCameraConfigs> virtualCameraConfigs)
        {
            virtualCameraConfigs.ForEach(configs =>
                _virtualCameras.Find(cam => cam.CameraType == configs.cameraType).SetConfigs(configs));

            SetCamera(CameraType.Game);
        }

        [Button, DisableInEditorMode]
        public void SetCamera(CameraType cameraType)
        {
            if (_currentCamera != null && _currentCamera.CameraType == cameraType) return;
            _currentCamera = _virtualCameras.Find(cam => cam.CameraType == cameraType);

            _virtualCameras.ForEach(cam => cam.Disable());
            _currentCamera.Enable();
        }

        [Button, DisableInEditorMode]
        public void ShakeCamera(float amplitude, float frequency, float duration)
        {
            _currentCamera.Shake(amplitude, frequency, duration);
        }

        [Button, DisableInEditorMode]
        public void StartShakeCamera(float amplitude, float frequency)
        {
            _currentCamera.StartShake(amplitude, frequency);
        }

        [Button, DisableInEditorMode]
        public void StopShakeCamera()
        {
            _currentCamera.StopShake();
        }
    }
}