using UnityEngine;

namespace Crico.AI.Behaviours
{
    public class FollowTargetBehaviour : AgentBehaviour
    {
        [SerializeField] float moveSpeed = 100f;
        [SerializeField] float arriveDist = 0.1f;

        Vector3 followDistVec;

        public override void StartRunning(Agent agent)
        {
            base.StartRunning(agent);

            Transform target = agent.GetComponent<TargetHolder>().target.transform;

            followDistVec = agent.transform.position - target.position;
        }

        public override void Process(Agent agent)
        {
            base.Process(agent);

            Transform target = agent.GetComponent<TargetHolder>().target.transform;

            Vector3 posToMoveTo = target.position + followDistVec;
            Vector3 distVec = posToMoveTo - agent.transform.position;
            float distMagSq = distVec.sqrMagnitude;

            if (distMagSq > arriveDist * arriveDist)
            {
                float movement = Time.deltaTime * moveSpeed;

                float dist = Mathf.Sqrt(distMagSq);
                if (movement > dist)
                    movement = dist;

                Vector3 dir = distVec * (1f / dist);

                Vector3 movementVec = dir * movement;
                Vector3 newPos = agent.transform.position + movementVec;
                agent.transform.position = newPos;
            }

        }
    }

}
