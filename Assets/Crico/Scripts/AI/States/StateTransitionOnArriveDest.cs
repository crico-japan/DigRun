using UnityEngine;

namespace Crico.AI.States
{
    public class StateTransitionOnArriveDest : StateTransition
    {
        [SerializeField] float arriveRange = 0.01f;
        [SerializeField] bool useTimeOut = false;
        [SerializeField] float timeOutTime = 5f;
        [SerializeField] bool xzOnly = false;

        float entryTime;
        public override void OnStateEnter(Agent agent)
        {
            base.OnStateEnter(agent);

            entryTime = Time.time;
        }

        public override bool CheckTransitionCondition(Agent agent)
        {
            bool doTransition = false;

            if (useTimeOut)
            {
                doTransition = Time.time - entryTime >= timeOutTime;
            }

            if (!doTransition)
            {
                Vector3 position = agent.transform.position;
                Vector3 dest = agent.Destination;
                
                if (xzOnly)
                    dest.y = position.y;

                float dist = (dest - position).magnitude;

                doTransition = dist <= arriveRange;
            }

            return doTransition;
        }
    }

}
