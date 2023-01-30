using LoxiGames.UI;

namespace WeaponTransformer.Shop
{
    public class ExitButton : ButtonBase
    {
        public override void OnClick()
        {
            base.OnClick();

            ShopManager.Instance.Deactivate();
        }
    }
}