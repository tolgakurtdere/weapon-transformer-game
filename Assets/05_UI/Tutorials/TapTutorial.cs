using DG.Tweening;
using UnityEngine;

namespace LoxiGames.UI.Tutorial
{
    public class TapTutorial : MonoBehaviour
    {
        [SerializeField] private RectTransform hand;
        [SerializeField] private float scale = 0.85f;
        [SerializeField] private float duration = 0.5f;
        private Vector3 _initHandScale;

        private void Awake()
        {
            _initHandScale = hand.localScale;
        }

        private void OnEnable()
        {
            hand.DOScale(new Vector3(scale, scale, scale), duration)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDisable()
        {
            hand.DOKill();
            hand.localScale = _initHandScale;
        }
    }
}