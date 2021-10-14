using Crico.AI.Behaviours;
using UnityEngine;

namespace Crico.AI.States
{
    public class StateTransitionOnBehaviourComplete : StateTransition
    {
        [SerializeField] AgentBehaviour behaviour = null;

        public override bool CheckTransitionCondition(Agent agent)
        {
            return behaviour.GetStatus() == AgentBehaviour.Status.SUCCESS
                || behaviour.GetStatus() == AgentBehaviour.Status.FAILURE;
        }

        public void SetTargetBehaviour(AgentBehaviour targetBehaviour)
        {
            behaviour = targetBehaviour;
        }
    }

}
