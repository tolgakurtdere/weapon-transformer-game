using System;
using DG.Tweening;
using LoxiGames.Utility;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace LoxiGames.Manager
{
    public class TransitionManager : MonoSingleton<TransitionManager>
    {
        [SerializeField, Required] private Image transitionImage;

        public void StartTransition(Action action = null, float fadeOutDuration = 0.5f, float fadeInDuration = 0.5f,
            float fadeInDelay = 0.1f)
        {
            transitionImage.enabled = true;

            var seq = DOTween.Sequence();
            seq.Append(transitionImage.DOFade(1f, fadeOutDuration)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() => { action?.Invoke(); }))
                .Append(transitionImage.DOFade(0f, fadeInDuration)
                    .SetEase(Ease.InQuad)
                    .SetDelay(fadeInDelay))
                .OnComplete(() => { transitionImage.enabled = false; });
        }
    }
}