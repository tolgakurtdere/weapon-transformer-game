using DG.Tweening;
using LoxiGames;
using LoxiGames.Manager;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using WeaponTransformer.Player;
using WeaponTransformer.Shop;

namespace WeaponTransformer
{
    public class MenuTutorial : Tutorial
    {
        [SerializeField, Required] private TextMeshProUGUI tutorialText;
        private const string Text1 = "TAP & HOLD TO SELECT WEAPON";
        private const string Text2 = "RELEASE ON DESIRED WEAPON";
        private const string PrefKeyDefault = "com.loxigames.menututorial1";
        private const string PrefKeyShop = "com.loxigames.menututorial2";

        private void Start()
        {
            LevelManager.OnLevelLoaded += OnLevelLoaded;
            LevelManager.OnLevelStopped += OnLevelStopped;
            WeaponMenuController.WeaponMenu.OnRadialMenuEnabled += OnRadialMenuEnabled;
            WeaponMenuController.WeaponMenu.OnRadialMenuDisabled += OnRadialMenuDisabled;
            BuyWeaponButton.OnWeaponBought += OnWeaponBought;
        }

        private void OnLevelLoaded()
        {
            if (PlayerPrefs.GetInt(PrefKeyDefault, 0) == 0) Show();
            else Hide();
        }

        private void OnLevelStopped(bool isSuccess)
        {
            Hide();
        }

        private void OnRadialMenuEnabled()
        {
            tutorialText.text = Text2;
        }

        private void OnRadialMenuDisabled()
        {
            Hide();
            PlayerPrefs.SetInt(PrefKeyDefault, 1);
        }

        private void OnWeaponBought(object sender, BuyWeaponButton.OnWeaponBoughtEventArgs e)
        {
            if (PlayerPrefs.GetInt(PrefKeyShop, 0) == 0)
            {
                Show();
                PlayerPrefs.SetInt(PrefKeyShop, 1);
            }
        }

        public override void Show()
        {
            base.Show();

            tutorialText.text = Text1;

            transform.DOScale(Vector3.one * 1.25f, 0.5f)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }

        public override void Hide()
        {
            base.Hide();

            transform.DOKill();
            transform.localScale = Vector3.one;
        }
    }
}