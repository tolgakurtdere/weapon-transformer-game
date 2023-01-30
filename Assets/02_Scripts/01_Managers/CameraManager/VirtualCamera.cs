using System;
using System.Collections;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LoxiGames.Manager
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public abstract class VirtualCamera : MonoBehaviour
    {
        [ShowInInspector, ReadOnly] protected CameraManager.CameraType Type;
        private CinemachineVirtualCamera _virtualCamera;
        private CinemachineTransposer _virtualCameraTransposer;
        private CinemachineBasicMultiChannelPerlin _virtualCameraNoise;
        private Coroutine _lastShakeCoroutine;

        public CameraManager.CameraType CameraType => Type;

        protected virtual void Awake()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
            _virtualCameraTransposer = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            _virtualCameraNoise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public virtual void SetConfigs(VirtualCameraConfigs configs)
        {
            _virtualCamera.Follow = configs.follow;
            _virtualCamera.LookAt = configs.lookAt;
        }

        public virtual void SetFollowOffsetX(float offsetX)
        {
            _virtualCameraTransposer.m_FollowOffset.x = offsetX;
        }

        public virtual void Enable()
        {
            _virtualCamera.enabled = true;
        }

        public virtual void Disable()
        {
            _virtualCamera.enabled = false;
        }

        public virtual void Shake(float amplitude, float frequency, float duration)
        {
            if (_lastShakeCoroutine != null) StopCoroutine(_lastShakeCoroutine);
            _lastShakeCoroutine = StartCoroutine(ShakeCoroutine(amplitude, frequency, duration));
        }

        public virtual void StartShake(float amplitude, float frequency)
        {
            _virtualCameraNoise.m_AmplitudeGain = amplitude;
            _virtualCameraNoise.m_FrequencyGain = frequency;
        }

        public virtual void StopShake()
        {
            _virtualCameraNoise.m_AmplitudeGain = 0f;
            _virtualCameraNoise.m_FrequencyGain = 0f;
        }

        private IEnumerator ShakeCoroutine(float amplitude, float frequency, float duration)
        {
            StartShake(amplitude, frequency);
            yield return new WaitForSeconds(duration);
            StopShake();
        }
    }

    [Serializable]
    public struct VirtualCameraConfigs
    {
        public CameraManager.CameraType cameraType;
        public Transform follow;
        public Transform lookAt;
    }
}