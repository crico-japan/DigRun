using Crico.GameEvents;
using UnityEngine;
using UnityEngine.Assertions;

namespace Crico.AI.Behaviours
{
    public class InvokeGameEvent : AgentBehaviour
    {
        [SerializeField] GameEvent gameEvent;

        private void Awake()
        {
            Assert.IsNotNull(gameEvent);
        }

        public override void StartRunning(Agent agent)
        {
            base.StartRunning(agent);
            gameEvent.Raise();
        }

    }

}
