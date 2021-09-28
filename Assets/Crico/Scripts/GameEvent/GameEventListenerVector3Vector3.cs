using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Crico.GameEvents
{
    public class GameEventListenerVector3Vector3 : MonoBehaviour, IGameEventListener<Vector3, Vector3>
    {
        [System.Serializable]
        public class UnityEventVector3Vector3 : UnityEvent<Vector3, Vector3> { }

        public GameEventVector3Vector3 gameEvent;
        public UnityEventVector3Vector3 eventResponse;

        private void Start()
        {
            Assert.IsNotNull(gameEvent);
        }

        public void OnEventRaised(Vector3 arg0, Vector3 arg1)
        {
            eventResponse.Invoke(arg0, arg1);
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
