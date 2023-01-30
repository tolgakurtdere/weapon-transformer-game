using LoxiGames.Manager;
using UnityEngine;

namespace WeaponTransformer
{
    public abstract class FirerBase : IFirer
    {
        protected readonly string bulletKey;
        protected readonly Transform muzzle;
        protected readonly ParticleSystem fireFx;

        protected FirerBase(string bulletKey, Transform muzzle, ParticleSystem fireFx)
        {
            this.bulletKey = bulletKey;
            this.muzzle = muzzle;
            this.fireFx = fireFx;
        }

        public virtual void Fire(Vector3 targetPos)
        {
            var muzzlePos = muzzle.position;
            var direction = (targetPos - muzzlePos).normalized;

            var bullet = ObjectPooler.Instance.SpawnFromPool(bulletKey, muzzlePos) as Bullet;

            bullet!.Fire(direction);

            fireFx.Play();
        }
    }
}