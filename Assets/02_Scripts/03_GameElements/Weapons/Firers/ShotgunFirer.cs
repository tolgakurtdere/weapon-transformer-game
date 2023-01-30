using LoxiGames.Manager;
using UnityEngine;

namespace WeaponTransformer
{
    public class ShotgunFirer : FirerBase
    {
        public ShotgunFirer(string bulletKey, Transform muzzle, ParticleSystem fireFx) : base(bulletKey, muzzle, fireFx)
        {
        }

        public override void Fire(Vector3 targetPos)
        {
            base.Fire(targetPos);

            var muzzlePos = muzzle.position;
            var direction = (targetPos - muzzlePos).normalized;

            var dir1 = Quaternion.Euler(0, 10, 0) * direction;
            var dir2 = Quaternion.Euler(0, -10, 0) * direction;

            var bullet1 = ObjectPooler.Instance.SpawnFromPool(bulletKey, muzzlePos) as Bullet;
            var bullet2 = ObjectPooler.Instance.SpawnFromPool(bulletKey, muzzlePos) as Bullet;

            bullet1!.Fire(dir1);
            bullet2!.Fire(dir2);
        }
    }
}