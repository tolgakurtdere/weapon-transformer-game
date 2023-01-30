using System;
using LoxiGames.UI;

namespace WeaponTransformer.Shop
{
    public class BuyHealthButton : UpgradeButtonBase
    {
        public static event EventHandler<OnHealthBoughtEventArgs> OnHealthBought;

        public class OnHealthBoughtEventArgs : EventArgs
        {
            public int Price { get; set; }
            public int HealthAmount { get; set; }
        }

        public override void OnClick()
        {
            base.OnClick();

            OnHealthBought?.Invoke(this, new OnHealthBoughtEventArgs
            {
                Price = price,
                HealthAmount = 10
            });
        }
    }
}