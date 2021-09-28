using UnityEngine;
using UnityEngine.AI;

namespace Crico.AI.Behaviours
{
    public class SetDestToTargetPos : AgentBehaviour
    {
        public override void StartRunning(Agent agent)
        {
            base.StartRunning(agent);

            RefreshDest(agent);
        }

        public override void Process(Agent agent)
        {
            base.Process(agent);

            RefreshDest(agent);
        }

        private void RefreshDest(Agent agent)
        {
            TargetHolder targetHolder = agent.GetComponent<TargetHolder>();
            Transform targetTrans = targetHolder.target.transform;

            agent.Destination = targetTrans.position;
        }
    }

}
