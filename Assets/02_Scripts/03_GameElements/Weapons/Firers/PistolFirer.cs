using UnityEngine;

namespace WeaponTransformer
{
    public class PistolFirer : FirerBase
    {
        public PistolFirer(string bulletKey, Transform muzzle, ParticleSystem fireFx) : base(bulletKey, muzzle, fireFx)
        {
        }
    }
}