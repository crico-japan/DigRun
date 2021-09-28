using Crico.AI;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Crico
{
    public class UnityEventOnAIStateChange : MonoBehaviour
    {
        [SerializeField] AgentAIInterface agentAI;
        [SerializeField] string originState = "StateName";
        [SerializeField] UnityEvent toDo = new UnityEvent();

        bool running;
        bool inStateToWatch;

        private void AssertInspectorVars()
        {
            Assert.IsNotNull(agentAI);
        }

        private void Awake()
        {
            AssertInspectorVars();
        }

        private void Start()
        {
            running = true;
        }

        private void Update()
        {
            if (!running)
                return;

            if (inStateToWatch)
            {
                if (agentAI.GetCurrentStateName() != originState)
                {
                    running = false;
                    toDo.Invoke();
                }
            }
            else
            {
                if (agentAI.GetCurrentStateName() == originState)
                    inStateToWatch = true;
            }
        }

    }

}
