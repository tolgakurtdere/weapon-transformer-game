using UnityEngine;

namespace WeaponTransformer
{
    public class RifleFirer : FirerBase
    {
        public RifleFirer(string bulletKey, Transform muzzle, ParticleSystem fireFx) : base(bulletKey, muzzle, fireFx)
        {
        }
    }
}