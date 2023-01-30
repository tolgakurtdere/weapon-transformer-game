using UnityEngine;

namespace WeaponTransformer
{
    public class SniperFirer : FirerBase
    {
        public SniperFirer(string bulletKey, Transform muzzle, ParticleSystem fireFx) : base(bulletKey, muzzle, fireFx)
        {
        }
    }
}