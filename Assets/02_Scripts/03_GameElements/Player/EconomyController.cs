using System;
using LoxiGames.Manager;
using Sirenix.OdinInspector;
using UnityEngine;
using WeaponTransformer.Collectable;
using WeaponTransformer.Shop;

namespace WeaponTransformer
{
    public class EconomyController : MonoBehaviour
    {
        private const string CoinKey = "com.loxigames.coinCount";
        public static event Action<int> OnCoinCountChanged;
        private int _coinCount;

        [ShowInInspector, ReadOnly]
        public int CoinCount
        {
            get => _coinCount;
            private set
            {
                _coinCount = value < 0 ? 0 : value;
                OnCoinCountChanged?.Invoke(value);

                UIManager.GameUI.SetCoinText(value.ToString());
                PlayerPrefs.SetInt(CoinKey, value);
            }
        }

        private void OnEnable()
        {
            BuyAmmoButton.OnAmmoBought += OnAmmoBought;
            BuyWeaponButton.OnWeaponBought += OnWeaponBought;
        }

        private void OnDisable()
        {
            BuyAmmoButton.OnAmmoBought -= OnAmmoBought;
            BuyWeaponButton.OnWeaponBought -= OnWeaponBought;
        }

        private void Start()
        {
            CoinCount = PlayerPrefs.GetInt(CoinKey, 0);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Coin coin))
            {
                coin.Collect(transform);
                CoinCount++;
            }
        }

        private void OnAmmoBought(object sender, BuyAmmoButton.OnAmmoBoughtEventArgs onAmmoBoughtEventArgs)
        {
            CoinCount -= onAmmoBoughtEventArgs.Price;
        }

        private void OnWeaponBought(object sender, BuyWeaponButton.OnWeaponBoughtEventArgs onWeaponBoughtEventArgs)
        {
            CoinCount -= onWeaponBoughtEventArgs.Price;
        }

        [Button(ButtonSizes.Gigantic)]
        private void GainCoin()
        {
            CoinCount += 100;
        }
    }
}