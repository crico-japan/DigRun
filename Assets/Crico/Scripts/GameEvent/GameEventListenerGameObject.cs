using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Crico.GameEvents
{
    public class GameEventListenerGameObject : MonoBehaviour, IGameEventListener<GameObject>
    {
        [System.Serializable]
        public class UnityEventGameObject : UnityEvent<GameObject> { }

        public GameEventGameObject gameEvent;
        public UnityEventGameObject eventResponse;

        private void Start()
        {
            Assert.IsNotNull(gameEvent);
        }

        public void OnEventRaised(GameObject value)
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
