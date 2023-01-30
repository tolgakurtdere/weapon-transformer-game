using LoxiGames.Utility;
using UnityEngine;

namespace LoxiGames.UI
{
    public abstract class UIElementBase : MonoBehaviour, ILoxiActivate
    {
        public bool IsActive { get; private set; }

        public virtual void Activate()
        {
            IsActive = true;
            gameObject.SetActive(true);
        }

        public virtual void Deactivate()
        {
            IsActive = false;
            gameObject.SetActive(false);
        }
    }
}