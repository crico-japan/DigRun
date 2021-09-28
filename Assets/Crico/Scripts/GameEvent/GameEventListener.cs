using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Crico.GameEvents
{
    public class GameEventListener : MonoBehaviour, IGameEventListener
    {
        public GameEvent gameEvent;
        public UnityEvent eventResponse;

        private void Start()
        {
            Assert.IsNotNull(gameEvent);
        }

        public void OnEventRaised()
        {
            eventResponse.Invoke();
        }

        private void OnEnable()
        {
            gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            gameEvent.UnregisterListener(this);
        }
    }
}
