using UnityEngine;

namespace Crico.AI.Behaviours
{
    public class SeekToTarget : MoveToDest
    {

        public override void Process(Agent agent)
        {
            Transform target = agent.GetComponent<TargetHolder>().target.transform;
            agent.Destination = target.position;

            base.Process(agent);
        }
    }

}
