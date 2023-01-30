using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LoxiGames.Manager
{
    public class InputManager : MonoBehaviour
    {
        public static event Action<Vector2> OnDrag;
        public static event Action OnPress;
        public static event Action OnRelease;

        private const int ResolutionReferenceX = 1080;
        private const int ResolutionReferenceY = 1920;
        private float _resolutionFactorX;
        private float _resolutionFactorY;
        private Camera _mainCamera;
        private Vector3 _lastPosition;
        private bool _isPressing;

        private void Awake()
        {
            _mainCamera = Camera.main;

            var pointerDownEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerDown};
            pointerDownEntry.callback.AddListener(data => { OnPointerDown(); });

            var pointerUpEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerUp};
            pointerUpEntry.callback.AddListener(data => { OnPointerUp(); });

            var eventTrigger = GetComponent<EventTrigger>();
            if (eventTrigger == null) eventTrigger = gameObject.AddComponent<EventTrigger>();

            eventTrigger.triggers.Add(pointerDownEntry);
            eventTrigger.triggers.Add(pointerUpEntry);
        }

        private void Start()
        {
            _resolutionFactorX = (float) ResolutionReferenceX / Screen.width;
            _resolutionFactorY = (float) ResolutionReferenceY / Screen.height;
        }

        private void OnPointerDown()
        {
            _lastPosition = _mainCamera.ScreenToViewportPoint(Input.mousePosition);
            _isPressing = true;
            OnPress?.Invoke();
        }

        private void OnPointerUp()
        {
            _isPressing = false;
            OnRelease?.Invoke();
        }

        private void Update()
        {
            if (!_isPressing) return;

            var currentPosition = _mainCamera.ScreenToViewportPoint(Input.mousePosition);
            var delta = currentPosition - _lastPosition;
            _lastPosition = currentPosition;
            OnDrag?.Invoke(new Vector2(delta.x * _resolutionFactorX, delta.y * _resolutionFactorY));
        }
    }
}