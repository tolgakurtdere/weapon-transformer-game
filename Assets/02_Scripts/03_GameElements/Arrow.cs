using DG.Tweening;
using UnityEngine;

namespace WeaponTransformer
{
    public class Arrow : MonoBehaviour
    {
        private void Start()
        {
            transform.DOLocalMoveY(-2, 0.8f)
                .SetRelative()
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}