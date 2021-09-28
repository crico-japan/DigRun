using Crico.AI.States;
using UnityEngine;
using UnityEngine.Assertions;

namespace Crico.AI
{
    public class AgentStatus : MonoBehaviour
    {
        [SerializeField] StateMachine stateMachine = null;
        [SerializeField] string[] inactiveStateNames = new string[] { };

        private void Awake()
        {
            Assert.IsNotNull(stateMachine);
        }

        public bool IsAgentActive()
        {
            string currentState = stateMachine.GetCurrentStateName();

            bool inactive = false;
            foreach (string stateName in inactiveStateNames)
            {
                if (stateName == currentState)
                {
                    inactive = true;
                    break;
                }
            }

            return !inactive;
        }

    }

}
