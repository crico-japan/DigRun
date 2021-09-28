using Crico.AI.States;
using UnityEngine;
using UnityEngine.Assertions;

namespace Crico.AI
{
    public class AgentAIInterface : MonoBehaviour
    {
        [SerializeField] StateMachine topStateMachine;

        private void AssertInspectorVars()
        {
            Assert.IsNotNull(topStateMachine);
        }

        private void Awake()
        {
            AssertInspectorVars();   
        }

        public string GetCurrentStateName()
        {
            return topStateMachine.GetCurrentStateName();
        }
    }

}
