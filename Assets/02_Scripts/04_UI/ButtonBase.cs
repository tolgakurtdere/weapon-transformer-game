using LoxiGames.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace LoxiGames.UI
{
    [RequireComponent(typeof(Button))]
    public abstract class ButtonBase : ClickableUIElementBase
    {
        private Button _button;

        private Button Button
        {
            get
            {
                if (!_button) _button = GetComponent<Button>();
                return _button;
            }
        }

        public override bool Interactable
        {
            get => Button.interactable;
            set => Button.interactable = value;
        }

        protected virtual void Awake()
        {
            Button.onClick.AddListener(OnClick);
        }

        public override void OnClick()
        {
            HapticManager.Selection();
        }
    }
}