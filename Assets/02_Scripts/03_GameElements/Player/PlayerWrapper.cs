using UnityEngine;

namespace WeaponTransformer.Player
{
    [RequireComponent(typeof(EconomyController))]
    [RequireComponent(typeof(PlayerHealthController))]
    [RequireComponent(typeof(FireController))]
    [RequireComponent(typeof(AnimatorController))]
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerWrapper : MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        private AnimatorController _animatorController;
        private FireController _fireController;
        private PlayerHealthController _playerHealthController;
        private EconomyController _economyController;

        public PlayerMovement PlayerMovement
        {
            get
            {
                if (!_playerMovement) _playerMovement = GetComponent<PlayerMovement>();
                return _playerMovement;
            }
        }

        public AnimatorController AnimatorController
        {
            get
            {
                if (!_animatorController) _animatorController = GetComponent<AnimatorController>();
                return _animatorController;
            }
        }

        public FireController FireController
        {
            get
            {
                if (!_fireController) _fireController = GetComponent<FireController>();
                return _fireController;
            }
        }

        public PlayerHealthController PlayerHealthController
        {
            get
            {
                if (!_playerHealthController) _playerHealthController = GetComponent<PlayerHealthController>();
                return _playerHealthController;
            }
        }

        public EconomyController EconomyController
        {
            get
            {
                if (!_economyController) _economyController = GetComponent<EconomyController>();
                return _economyController;
            }
        }
    }
}