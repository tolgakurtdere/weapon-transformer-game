using Sirenix.OdinInspector;
using UnityEngine;
using WeaponTransformer.Player;
using WeaponTransformer.Shop;

namespace WeaponTransformer
{
    public abstract class FirearmWeaponBase : WeaponBase
    {
        [SerializeField, Min(0)] private int ammo = 10;
        [SerializeField, Min(0)] private int boughtAmmoCount = 10;
        [SerializeField, Required] protected string bulletKey;
        [SerializeField, Required] protected Transform muzzle;
        [SerializeField, Required] protected ParticleSystem fireFx;

        protected IFirer firer;

        public int Ammo
        {
            get => ammo;
            set
            {
                ammo = value;
                OnAmmoChanged(value);
            }
        }

        private void Awake()
        {
            WeaponMenuController.OnWeaponAdded += OnWeaponAdded;
            BuyAmmoButton.OnAmmoBought += OnAmmoBought;

            Deactivate();
        }

        private void OnDestroy()
        {
            WeaponMenuController.OnWeaponAdded -= OnWeaponAdded;
            BuyAmmoButton.OnAmmoBought -= OnAmmoBought;
        }

        private void OnWeaponAdded(WeaponData weaponData)
        {
            if (weaponData != Data) return;

            Initialize();
        }

        private void OnAmmoBought(object sender, BuyAmmoButton.OnAmmoBoughtEventArgs onAmmoBoughtEventArgs)
        {
            if (Data.IsUnlocked)
            {
                Ammo += boughtAmmoCount;
            }
        }

        protected override void Initialize()
        {
            base.Initialize();

            var savedAmmo = PlayerPrefs.GetInt(Data.ButtonInfo.key + "ammo", -1);
            if (savedAmmo > -1) ammo = savedAmmo;

            Ammo = ammo;
        }

        protected override bool Fire(Vector3 targetPos)
        {
            //if no ammo, cannot fire
            if (Ammo <= 0)
            {
                //TODO: No ammo sound, haptic etc.
                return false;
            }

            targetPos.y = muzzle.position.y;

            firer.Fire(targetPos);

            Ammo--;

            return true;
        }

        private void OnAmmoChanged(int newAmmoCount)
        {
            Data.ButtonInfo.UpdateText(newAmmoCount.ToString());
            PlayerPrefs.SetInt(Data.ButtonInfo.key + "ammo", newAmmoCount);
        }


        #region Development - DrawGizmos

#if UNITY_EDITOR
        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(muzzle.position, 0.05f);
        }
#endif

        #endregion
    }
}