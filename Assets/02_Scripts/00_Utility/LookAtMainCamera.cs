using UnityEngine;

namespace LoxiGames.Utility
{
    public class LookAtMainCamera : MonoBehaviour
    {
        private Vector3 _offset;
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void LateUpdate()
        {
            transform.forward = _mainCamera.transform.forward;
        }
    }
}