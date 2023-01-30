using UnityEngine;
using UnityEngine.Events;

namespace LoxiGames.Utility.Events
{
    public class GameEventListener : MonoBehaviour
    {
        [Tooltip("Event to register with.")] [SerializeField]
        private GameEvent gameEvent;

        [Tooltip("Response to invoke when Event is raised.")] [SerializeField]
        private UnityEvent response;

        private void OnEnable()
        {
            gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            gameEvent.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            response.Invoke();
        }
    }
}