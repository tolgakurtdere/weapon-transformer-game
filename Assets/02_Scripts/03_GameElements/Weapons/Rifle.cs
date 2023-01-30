namespace WeaponTransformer
{
    public class Rifle : FirearmWeaponBase
    {
        protected override void Initialize()
        {
            base.Initialize();

            firer = new RifleFirer(bulletKey, muzzle, fireFx);
        }
    }
}