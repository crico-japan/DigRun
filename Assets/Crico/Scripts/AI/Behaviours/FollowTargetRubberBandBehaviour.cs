using UnityEngine;

namespace Crico.AI.Behaviours
{
    public class FollowTargetRubberBandBehaviour : AgentBehaviour
    {
        [SerializeField] float moveSpeed = 10f;
        [SerializeField] float arriveDist = 0.1f;

        [SerializeField] float rotation = 0f;

        Vector3 followDistVec;
        Quaternion baseRotation;

        public override void StartRunning(Agent agent)
        {
            base.StartRunning(agent);

            Transform target = agent.GetComponent<TargetHolder>().target.transform;

            followDistVec = agent.transform.position - target.position;
            baseRotation = agent.transform.rotation;
        }

        public void SetRotation(float amount)
        {
            this.rotation = amount;
        }


        public override void Process(Agent agent)
        {
            base.Process(agent);

            Transform target = agent.GetComponent<TargetHolder>().target.transform;

            Quaternion rotationMod = Quaternion.Euler(0f, rotation, 0f);
            Vector3 thisFollowDistVec = rotationMod * followDistVec;
            Quaternion camRotation = rotationMod * baseRotation;

            Vector3 posToMoveTo = target.position + thisFollowDistVec;
            Vector3 distVec = posToMoveTo - agent.transform.position;
            float distMagSq = distVec.sqrMagnitude;

            if (distMagSq > arriveDist * arriveDist)
            {
                agent.transform.rotation = camRotation;
                float dist = Mathf.Sqrt(distMagSq);

                float thisMoveSpeed = moveSpeed * dist;
                float movement = Time.deltaTime * thisMoveSpeed;

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
