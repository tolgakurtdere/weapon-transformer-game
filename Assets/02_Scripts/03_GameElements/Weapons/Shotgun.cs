namespace WeaponTransformer
{
    public class Shotgun : FirearmWeaponBase
    {
        protected override void Initialize()
        {
            base.Initialize();

            firer = new ShotgunFirer(bulletKey, muzzle, fireFx);
        }
    }
}