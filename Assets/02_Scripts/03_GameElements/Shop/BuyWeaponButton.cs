using System;
using LoxiGames.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace WeaponTransformer.Shop
{
    public class BuyWeaponButton : UpgradeButtonBase
    {
        public static event EventHandler<OnWeaponBoughtEventArgs> OnWeaponBought;
        [SerializeField, Required] private WeaponData weaponData;

        protected override bool AdditionalInteractableCondition => !weaponData.IsUnlocked;

        public class OnWeaponBoughtEventArgs : EventArgs
        {
            public int Price { get; set; }
            public WeaponData WeaponData { get; set; }
        }

        public override void OnClick()
        {
            base.OnClick();

            weaponData.Unlock();

            OnWeaponBought?.Invoke(this, new OnWeaponBoughtEventArgs
            {
                Price = price,
                WeaponData = weaponData
            });

            Debug.Log($"<color=blue>New weapon is unlocked! : {weaponData.ButtonInfo.name}</color>");
        }
    }
}