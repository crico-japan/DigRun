using System.Collections.Generic;
using UnityEngine;

namespace Crico.AI.Behaviours
{
    public class PlayAnimation : AgentBehaviour
    {
        [SerializeField] string triggerToSet = "trigger";

        public override void StartRunning(Agent agent)
        {
            base.StartRunning(agent);

            agent.SetAnimationTrigger(triggerToSet);
        }

        public override Status GetStatus()
        {
            return Status.SUCCESS;
        }

    }

}
