using System.Collections.Generic;
using UnityEngine;

namespace Crico.AI.Behaviours
{
    public class PlayRandomAnimation : AgentBehaviour
    {
        [SerializeField] List<string> triggers = new List<string>();

        public override void StartRunning(Agent agent)
        {
            base.StartRunning(agent);

            if (triggers.Count == 0)
                return;

            int randomIndex = Random.Range(0, triggers.Count);
            string trigger = triggers[randomIndex];
            agent.SetAnimationTrigger(trigger);
        }

    }

}
