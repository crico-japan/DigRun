using UnityEngine;

namespace Crico.AI.States
{
    public class StateTransitionOnAnyActiveAgents : StateTransition
    {
        [SerializeField] int waitFrames = 1;
        [SerializeField] ActiveAgentStatusChecker statusChecker;
        [SerializeField] bool expectedValue;

        int framesLeft = 0;

        public override void OnStateEnter(Agent agent)
        {
            base.OnStateEnter(agent);

            framesLeft = waitFrames;
        }

        public override bool CheckTransitionCondition(Agent agent)
        {
            if (framesLeft > 0)
            {
                --framesLeft;
                return false;
            }

            return statusChecker.AreAnyAgentsActive() == expectedValue;
        }

    }

}
