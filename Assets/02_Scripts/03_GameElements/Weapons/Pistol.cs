namespace WeaponTransformer
{
    public class Pistol : FirearmWeaponBase
    {
        protected override void Initialize()
        {
            base.Initialize();

            firer = new PistolFirer(bulletKey, muzzle, fireFx);
        }
    }
}