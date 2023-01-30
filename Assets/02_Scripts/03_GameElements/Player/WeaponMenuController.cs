using System;
using System.Collections.Generic;
using System.Linq;
using LoxiGames.Manager;
using Sirenix.OdinInspector;
using UnityEngine;
using WeaponTransformer.Shop;

namespace WeaponTransformer.Player
{
    public class WeaponMenuController : MonoBehaviour
    {
        public static event Action<WeaponData> OnWeaponAdded;
        public static event Action<WeaponData> OnWeaponSelected;
        public static UltimateRadialMenu WeaponMenu => UltimateRadialMenu.GetUltimateRadialMenu("WeaponMenu");

        [SerializeField, Range(0.1f, 1f)] private float activateTime = 0.5f;
        [ShowInInspector, ReadOnly] private WeaponData _selectedWeaponData;
        [ShowInInspector, ReadOnly] private readonly Dictionary<string, WeaponData> _weaponDictionary = new();
        private float _time;
        private bool _isTimerStarted;
        private Joystick _joystick;

        [SerializeField, Required] private List<WeaponData> weaponDataList;

        private void OnEnable()
        {
            LevelManager.OnLevelLoaded += OnLevelLoaded;
            BuyWeaponButton.OnWeaponBought += OnWeaponBought;

            _joystick.OnClicked += StartMenuTimer;
            _joystick.OnReleased += ResetMenuTimer;
        }

        private void OnDisable()
        {
            LevelManager.OnLevelLoaded -= OnLevelLoaded;
            BuyWeaponButton.OnWeaponBought -= OnWeaponBought;

            _joystick.OnClicked -= StartMenuTimer;
            _joystick.OnReleased -= ResetMenuTimer;
        }

        private void Awake()
        {
            _joystick = GameManager.Joystick;
        }

        private void Start()
        {
            //WeaponMenu.RemoveAllRadialButtons();

            foreach (var weaponData in weaponDataList)
            {
                AddNewWeapon(weaponData);
            }

            WeaponMenu.OnRadialMenuEnabled += OnRadialMenuEnabled;
            WeaponMenu.OnRadialMenuDisabled += OnRadialMenuDisabled;
            WeaponMenu.OnRadialButtonEnter += OnRadialButtonEnter;

            var firstUnlockedWeapon = weaponDataList.FirstOrDefault(data => data.IsUnlocked);
            if (firstUnlockedWeapon)
            {
                SelectWeapon(firstUnlockedWeapon.ButtonInfo.key);
            }
        }

        private void Update()
        {
            if (_joystick.Direction != Vector2.zero) ResetMenuTimer();
            if (_isTimerStarted)
            {
                _time += Time.deltaTime;
                if (_time >= activateTime)
                {
                    _joystick.OnPointerUp(null);
                    WeaponMenu.SetPosition(Input.mousePosition);
                    WeaponMenu.EnableRadialMenu();
                    ResetMenuTimer();
                }
            }
        }

        private void OnLevelLoaded()
        {
            ResetMenuTimer();
        }

        private void AddNewWeapon(WeaponData weaponData)
        {
            if (!weaponData.IsUnlocked) return;

            WeaponMenu.RegisterToRadialMenu(SelectWeapon, weaponData.ButtonInfo, weaponDataList.IndexOf(weaponData));
            _weaponDictionary.Add(weaponData.ButtonInfo.key, weaponData);

            OnWeaponAdded?.Invoke(weaponData);
        }

        private void SelectWeapon(string key)
        {
            if (!_weaponDictionary.ContainsKey(key))
            {
                Debug.LogError("Key does not exist in the dictionary: " + key);
                return;
            }

            _selectedWeaponData = _weaponDictionary[key];
            OnWeaponSelected?.Invoke(_selectedWeaponData);
        }

        private void OnWeaponBought(object sender, BuyWeaponButton.OnWeaponBoughtEventArgs onWeaponBoughtEventArgs)
        {
            AddNewWeapon(onWeaponBoughtEventArgs.WeaponData);
            SelectWeapon(onWeaponBoughtEventArgs.WeaponData.ButtonInfo.key);
        }

        private void OnRadialMenuEnabled()
        {
            Time.timeScale = 0.2f;
        }

        private void OnRadialMenuDisabled()
        {
            Time.timeScale = 1f;
        }

        private void OnRadialButtonEnter(int buttonIndex)
        {
            HapticManager.Selection();
        }

        private void StartMenuTimer()
        {
            _isTimerStarted = true;
        }

        private void ResetMenuTimer()
        {
            _isTimerStarted = false;
            _time = 0f;
        }
    }
}