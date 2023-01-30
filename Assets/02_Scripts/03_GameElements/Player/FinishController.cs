using DG.Tweening;
using LoxiGames.Manager;
using UnityEngine;

namespace WeaponTransformer.Player
{
    public class FinishController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out FinishArea finishArea))
            {
                finishArea.Deactivate();
                transform.DOMove(finishArea.FinishPosition, 3f);
                transform.DOLookAt(finishArea.FinishPosition, 0.2f);

                LevelManager.StopLevel(true);
                TransitionManager.Instance.StartTransition(() => LevelManager.Instance.LoadLevel(true), 2);
            }
        }
    }
}