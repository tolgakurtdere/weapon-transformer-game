using LoxiGames.Manager;
using LoxiGames.Utility;

namespace WeaponTransformer.Shop
{
    public class ShopManager : MonoSingleton<ShopManager>, ILoxiActivate
    {
        public bool IsActive { get; private set; }

        private void Start()
        {
            Deactivate();
        }

        public void Activate()
        {
            IsActive = true;
            gameObject.SetActive(true);

            HapticManager.Shop();
        }

        public void Deactivate()
        {
            IsActive = false;
            gameObject.SetActive(false);
        }
    }
}