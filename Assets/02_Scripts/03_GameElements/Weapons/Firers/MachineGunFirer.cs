using UnityEngine;

namespace WeaponTransformer
{
    public class MachineGunFirer : FirerBase
    {
        public MachineGunFirer(string bulletKey, Transform muzzle, ParticleSystem fireFx)
            : base(bulletKey, muzzle, fireFx)
        {
        }
    }
}