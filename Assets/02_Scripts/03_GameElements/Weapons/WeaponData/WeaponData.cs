using Sirenix.OdinInspector;
using UnityEngine;

namespace WeaponTransformer
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Loxi Games/Weapon Transformer/Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField, Min(0.1f)] private float range = 1f;
        [SerializeField, Range(0.2f, 20)] private float fireRate = 1f;
        [SerializeField, Required] private LayerMask targetLayer;
        [SerializeField, Required] private string animationParameter;
        [SerializeField, Required] private bool isDefault;

        public bool IsUnlocked => isDefault || PlayerPrefs.GetInt(buttonInfo.key, 0) == 1;

        public void Unlock()
        {
            PlayerPrefs.SetInt(buttonInfo.key, 1);
        }

        [SerializeField, FoldoutGroup("Button Info")]
        private UltimateRadialButtonInfo buttonInfo;

        public float Range => range;
        public float FireRate => fireRate;
        public LayerMask TargetLayer => targetLayer;
        public string AnimationParameter => animationParameter;
        public UltimateRadialButtonInfo ButtonInfo => buttonInfo;
    }
}