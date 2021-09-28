using UnityEngine;

namespace Crico.AI.Behaviours
{
    public class MoveToDest : AgentBehaviour
    {
        [SerializeField] float arriveTolerance = 0.01f;
        [SerializeField] float speed = 1f;
        [SerializeField] bool xzOnly;

        public override void Process(Agent agent)
        {
            base.Process(agent);

            Vector3 dest = agent.Destination;

            Vector3 position = agent.transform.position;

            if (xzOnly)
                dest.y = position.y;

            Vector3 dist = dest - position;

            float distMag = dist.magnitude;

            if (distMag <= arriveTolerance)
                return;

            Vector3 dir = dist.normalized;

            float movementMag = speed * Time.deltaTime;

            if (movementMag > distMag)
                movementMag = distMag;

            Vector3 movement = dir * movementMag;

            Vector3 newPos = position + movement;

            agent.transform.position = newPos;
        }

    }

}
