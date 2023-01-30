using Sirenix.OdinInspector;
using UnityEngine;

namespace LoxiGames
{
    public class MoneyUI : PoolObject
    {
        [SerializeField, Required] private RectTransform rectTransform;

        public RectTransform RectTransform => rectTransform;
    }
}