namespace WeaponTransformer
{
    public class MachineGun : FirearmWeaponBase
    {
        protected override void Initialize()
        {
            base.Initialize();

            firer = new MachineGunFirer(bulletKey, muzzle, fireFx);
        }
    }
}