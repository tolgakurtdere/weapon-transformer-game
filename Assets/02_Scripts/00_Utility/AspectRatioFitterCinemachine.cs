using Cinemachine;
using UnityEngine;

namespace LoxiGames.Utility
{
    [RequireComponent(typeof(Camera))]
    public class AspectRatioFitterCinemachine : MonoBehaviour
    {
        private const float Multiplier = 1.6f;
        private const float RefRatio = 16f / 9f;
        private const float RefDif = 1f / 9f;

        private Camera _camera;
        private CinemachineVirtualCamera[] _virtualCameras;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _virtualCameras = FindObjectsOfType<CinemachineVirtualCamera>();
        }

        private void Start()
        {
            if (_camera.orthographic)
            {
                FitCameraForOrthographic();
            }
            else
            {
                FitCameraForPerspective();
            }
        }

        private void FitCameraForOrthographic()
        {
            //Find aspect ratio
            var ratio = Screen.height / (float) Screen.width;
            var difRatio = Mathf.Abs(RefRatio - ratio);
            var difference = difRatio / RefDif;

            //Set size of Orthographic Camera
            if (ratio > RefRatio) _camera.orthographicSize += Multiplier * difference;
            else _camera.orthographicSize -= Multiplier * difference;
        }

        private void FitCameraForPerspective()
        {
            //Find aspect ratio
            var ratio = Screen.height / (float) Screen.width;
            var difRatio = Mathf.Abs(RefRatio - ratio);
            var difference = difRatio / RefDif;

            // Set size of Perspective Camera
            foreach (var cam in _virtualCameras)
            {
                if (ratio > RefRatio) cam.m_Lens.FieldOfView += Multiplier * difference;
                else cam.m_Lens.FieldOfView -= Multiplier * difference;
            }
        }
    }
}