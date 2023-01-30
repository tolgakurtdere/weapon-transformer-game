using DG.Tweening;
using LoxiGames.Manager;
using UnityEngine;

namespace LoxiGames.UI.Tutorial
{
    public class DragTutorial : MonoBehaviour
    {
        [SerializeField] private RectTransform hand;
        [SerializeField] private RectTransform[] points;
        [SerializeField] private float duration = 3f;
        private Vector2 _initHandPos;
        private Joystick _joystick;

        private void Awake()
        {
            _initHandPos = hand.anchoredPosition;
            _joystick = GameManager.Joystick;
        }

        private void OnEnable()
        {
            _joystick.OnClicked += OnJoystickClicked;

            var path = new Vector3[points.Length];
            for (var i = 0; i < points.Length; i++)
            {
                path[i] = points[i].localPosition;
            }

            hand.DOLocalPath(path, duration, PathType.CatmullRom)
                .SetEase(Ease.Linear)
                .SetLoops(-1);
        }

        private void OnDisable()
        {
            _joystick.OnClicked -= OnJoystickClicked;

            hand.DOKill();
            hand.anchoredPosition = _initHandPos;
        }

        private void OnJoystickClicked()
        {
            gameObject.SetActive(false);
        }
    }
}