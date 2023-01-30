using System;
using LoxiGames.UI;

namespace WeaponTransformer.Shop
{
    public class BuyAmmoButton : UpgradeButtonBase
    {
        public static event EventHandler<OnAmmoBoughtEventArgs> OnAmmoBought;

        public class OnAmmoBoughtEventArgs : EventArgs
        {
            public int Price { get; set; }
        }

        public override void OnClick()
        {
            base.OnClick();

            OnAmmoBought?.Invoke(this, new OnAmmoBoughtEventArgs
            {
                Price = price,
            });
        }
    }
}