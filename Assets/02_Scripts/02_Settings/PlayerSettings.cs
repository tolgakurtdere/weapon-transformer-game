using UnityEngine;

namespace LoxiGames.Settings
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Loxi Games/Settings/Player Settings")]
    public class PlayerSettings : ScriptableObject
    {
        [SerializeField] private float defaultSpeed;
        [SerializeField] private float movementSensitivity;
        [SerializeField] private float rotationSensitivity;
        [SerializeField] private float localPositionXBorder;
        [SerializeField, Range(0, 90)] private float localRotationYBorder;
        [SerializeField, Range(1, 360)] private float defaultRotationTweenSpeed;

        public float DefaultSpeed => defaultSpeed;
        public float MovementSensitivity => movementSensitivity;
        public float RotationSensitivity => rotationSensitivity;
        public float LocalPositionXBorder => localPositionXBorder;
        public float LocalRotationYBorder => localRotationYBorder;
        public float DefaultRotationTweenSpeed => defaultRotationTweenSpeed;
    }
}