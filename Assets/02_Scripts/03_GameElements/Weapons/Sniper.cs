namespace WeaponTransformer
{
    public class Sniper : FirearmWeaponBase
    {
        protected override void Initialize()
        {
            base.Initialize();

            firer = new SniperFirer(bulletKey, muzzle, fireFx);
        }
    }
}