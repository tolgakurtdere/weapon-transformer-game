using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LoxiGames.Utility
{
    public class HealthBar : MonoBehaviour, ILoxiActivate
    {
        [SerializeField, Required] private Image fill;
        [SerializeField, Required] private TextMeshProUGUI text;

        public bool IsActive { get; private set; }

        public void Set(int currentHealth, int maxHealth)
        {
            fill.fillAmount = currentHealth / (float) maxHealth;
            text.text = currentHealth.ToString();
        }

        public void Activate()
        {
            IsActive = true;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            IsActive = false;
            gameObject.SetActive(false);
        }
    }
}