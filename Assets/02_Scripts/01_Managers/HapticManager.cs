using MoreMountains.NiceVibrations;

namespace LoxiGames.Manager
{
    public static class HapticManager
    {
        public static void Shop()
        {
            MMVibrationManager.Haptic(HapticTypes.LightImpact);
        }

        public static void Selection()
        {
            MMVibrationManager.Haptic(HapticTypes.Selection);
        }

        public static void Collect()
        {
            MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        }

        public static void Fire()
        {
            MMVibrationManager.Haptic(HapticTypes.LightImpact);
        }

        public static void Kill()
        {
            //MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        }

        public static void Finish()
        {
            MMVibrationManager.Haptic(HapticTypes.SoftImpact);
        }

        public static void Explosion()
        {
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        }

        public static void Detected()
        {
            MMVibrationManager.Haptic(HapticTypes.Warning);
        }

        public static void TakeDamage()
        {
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        }
    }
}