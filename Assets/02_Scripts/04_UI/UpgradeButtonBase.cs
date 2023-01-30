using TMPro;
using UnityEngine;
using WeaponTransformer;

namespace LoxiGames.UI
{
    public abstract class UpgradeButtonBase : ButtonBase
    {
        [SerializeField, Min(1)] protected int price = 10;
        private TextMeshProUGUI _priceText;

        protected virtual bool AdditionalInteractableCondition => true;

        protected override void Awake()
        {
            EconomyController.OnCoinCountChanged += UpdateButtonInfo;

            base.Awake();

            _priceText = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void OnDestroy()
        {
            EconomyController.OnCoinCountChanged -= UpdateButtonInfo;
        }

        private void UpdateButtonInfo(int coinCount)
        {
            _priceText.text = price.ToString();
            Interactable = price <= coinCount && AdditionalInteractableCondition;
        }
    }
}