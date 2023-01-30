using DG.Tweening;
using UnityEngine;

namespace LoxiGames.UI.Tutorial
{
    public class SlideTutorial : MonoBehaviour
    {
        [SerializeField] private RectTransform hand;
        [SerializeField] private RectTransform point;
        [SerializeField] private float duration = 1.5f;
        private Vector2 _initHandPos;

        private void Awake()
        {
            _initHandPos = hand.anchoredPosition;
        }

        private void OnEnable()
        {
            hand.DOAnchorPosX(point.anchoredPosition.x, duration)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDisable()
        {
            hand.DOKill();
            hand.anchoredPosition = _initHandPos;
        }
    }
}