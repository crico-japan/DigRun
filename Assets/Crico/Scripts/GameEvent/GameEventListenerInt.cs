using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Crico.GameEvents
{
    public class GameEventListenerInt : MonoBehaviour, IGameEventListener<int>
    {
        [System.Serializable]
        public class UnityEventInt : UnityEvent<int> { }

        public GameEventInt gameEvent;
        public UnityEventInt eventResponse;

        private void Start()
        {
            Assert.IsNotNull(gameEvent);
        }

        public void OnEventRaised(int value)
        {
            eventResponse.Invoke(value);
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
