using UnityEngine;

namespace Crico.AI.States
{
    public class StateTransitionOnArriveEndOfTrack : StateTransition
    {
        [SerializeField] float distFromEnd = 0f;
        [SerializeField] float arriveRange = 0.01f;
        [SerializeField] bool useTimeOut = false;
        [SerializeField] float timeOutTime = 5f;
        [SerializeField] bool xzOnly = false;

        float entryTime;
        Vector3 dest;

        public override void OnStateEnter(Agent agent)
        {
            base.OnStateEnter(agent);

            entryTime = Time.time;

            TrackHolder trackHolder = agent.GetComponent<TrackHolder>();
            float lengthOfTrackToStopAt = trackHolder.CalcTrackLength() - distFromEnd;

            Vector3 direction, normal;
            dest = trackHolder.GetPositionAndOrientation(lengthOfTrackToStopAt, out direction, out normal);
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

                Vector3 distVec = (dest - position);

                if (xzOnly)
                    distVec.y = 0f;

                float dist = distVec.magnitude;

                doTransition = dist <= arriveRange;
            }

            return doTransition;
        }
    }

}
