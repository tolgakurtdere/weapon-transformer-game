using System.Collections;
using DG.Tweening;
using LoxiGames;
using UnityEngine;

namespace WeaponTransformer.Collectable
{
    [RequireComponent(typeof(Collider))]
    public class Coin : PoolObject
    {
        private Collider _collider;

        public Collider Collider
        {
            get
            {
                if (!_collider) _collider = GetComponent<Collider>();
                return _collider;
            }
        }

        public void Collect(Transform target)
        {
            Collider.enabled = false;
            StartCoroutine(GoToTarget(target));
        }

        public override void Activate()
        {
            base.Activate();

            var r = Random.insideUnitSphere * 3;
            r.y = 0;

            transform.DOJump(transform.position + r, 2, 1, 0.7f)
                .OnComplete(() =>
                {
                    transform.DORotate(new Vector3(0, 360, 0), 1f)
                        .SetRelative()
                        .SetLoops(-1);
                    Collider.enabled = true;
                });
        }

        public override void Deactivate()
        {
            transform.DOKill();

            base.Deactivate();
        }

        private IEnumerator GoToTarget(Transform target)
        {
            while (true)
            {
                yield return null;

                transform.position = Vector3.MoveTowards(transform.position, target.position, 20 * Time.deltaTime);

                if ((transform.position - target.position).sqrMagnitude < 0.1f)
                {
                    Deactivate();

                    break;
                }
            }
        }
    }
}