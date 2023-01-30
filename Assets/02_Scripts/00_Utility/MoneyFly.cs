using System.Collections.Generic;
using DG.Tweening;
using LoxiGames.Manager;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LoxiGames.Utility
{
    public class MoneyFly : MonoSingleton<MoneyFly>
    {
        [SerializeField, Required] private RectTransform moneyTargetPoint;
        [SerializeField] private float spreadRadius = 150;
        [SerializeField] private float spreadDuration = 0.1f;
        [SerializeField] private float flyDuration = 0.6f;
        private Camera _mainCamera;

        protected override void Awake()
        {
            base.Awake();
            _mainCamera = Camera.main;
        }

        [Button, DisableInEditorMode]
        public void Spawn(Vector3 worldPoint, int count = 5)
        {
            var spreadSeq = DOTween.Sequence().SetEase(Ease.InOutSine);
            var flySeq = DOTween.Sequence().SetEase(Ease.InOutSine);
            var moneyUIs = new List<MoneyUI>();
            var rects = new List<RectTransform>();

            for (var i = 0; i < count; i++)
            {
                var moneyUI = (MoneyUI) ObjectPooler.Instance.SpawnFromPool("moneyUI",
                    _mainCamera.WorldToScreenPoint(worldPoint));
                var rect = moneyUI.RectTransform;
                rect.SetParent(moneyTargetPoint.parent);
                rect.localScale = Vector3.one;
                var spread = Random.insideUnitCircle * spreadRadius;

                spreadSeq.Join(rect.DOAnchorPos(spread, spreadDuration).SetRelative());
                moneyUIs.Add(moneyUI);
                rects.Add(rect);
            }

            foreach (var rect in rects)
            {
                flySeq.Join(rect.DOAnchorPos(moneyTargetPoint.anchoredPosition, flyDuration));
            }

            spreadSeq.Append(flySeq).OnComplete(() =>
            {
                foreach (var moneyUI in moneyUIs)
                {
                    moneyUI.Deactivate();
                }
            });
        }
    }
}