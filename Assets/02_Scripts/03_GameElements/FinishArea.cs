using DG.Tweening;
using LoxiGames.Manager;
using LoxiGames.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace WeaponTransformer
{
    [RequireComponent(typeof(Collider))]
    public class FinishArea : MonoBehaviour, ILoxiActivate
    {
        [SerializeField, Required] private GameObject finishObjects;
        [SerializeField, Required] private GameObject bridge;
        [SerializeField, Required] private Transform arrow;
        [SerializeField, Required] private Transform finishImage;
        [SerializeField, Required] private Transform finishPoint;
        private Collider _collider;

        public Vector3 FinishPosition => finishPoint.position;
        public bool IsActive { get; private set; }

        private void Awake()
        {
            _collider = GetComponent<Collider>();

            Deactivate();
            bridge.SetActive(false);
        }

        public void Activate()
        {
            IsActive = true;
            _collider.enabled = true;

            finishObjects.SetActive(true);
            bridge.SetActive(true);

            arrow.DOLocalMoveY(-2, 0.8f)
                .SetEase(Ease.InOutSine)
                .SetRelative()
                .SetLoops(-1, LoopType.Yoyo);

            finishImage.DOScale(Vector3.one, 0.8f)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);

            HapticManager.Finish();
        }

        public void Deactivate()
        {
            IsActive = false;
            _collider.enabled = false;

            finishObjects.SetActive(false);

            arrow.DOKill();
            finishImage.DOKill();
        }


        #region Development - DrawGizmos

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(finishPoint.position, 0.5f);
        }
#endif

        #endregion
    }
}