using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Crico.GameEvents
{
    public class GameEventListenerVector3 : MonoBehaviour, IGameEventListener<Vector3>
    {
        [System.Serializable]
        public class UnityEventVector3 : UnityEvent<Vector3> { }

        public GameEventVector3 gameEvent;
        public UnityEventVector3 eventResponse;

        private void Start()
        {
            Assert.IsNotNull(gameEvent);
        }

        public void OnEventRaised(Vector3 value)
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
