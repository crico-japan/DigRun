using Crico.GameEvents;
using UnityEngine;
using UnityEngine.Assertions;

namespace Crico.AI
{
    public class GameEventHolderGameObject : MonoBehaviour
    {
        [SerializeField] GameEventGameObject eventToInvoke;
        [SerializeField] GameObject gameObjectToInvokeWith;

        private void Awake()
        {
            Assert.IsNotNull(eventToInvoke);
            Assert.IsNotNull(gameObjectToInvokeWith);
        }

        public void InvokeEvent()
        {
            eventToInvoke.Raise(gameObjectToInvokeWith);
        }
    }

}
