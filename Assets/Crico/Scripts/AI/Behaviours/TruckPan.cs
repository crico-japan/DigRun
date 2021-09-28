using UnityEngine;

namespace Crico.AI.Behaviours
{
    public class TruckPan : AgentBehaviour
    {
        [SerializeField] float centreDistance = 0f;
        [SerializeField] float moveSpeed = 10f;
        [SerializeField] float arriveDist = 0.1f;
        [SerializeField] float rotateSpeed = 180f;
        [SerializeField] float arriveAngle = 1f;
        [SerializeField] Vector3 rotationOffset = new Vector3();

        Vector3 centre;

        Vector3 moveTarget;

        Quaternion targetRot;

        public override void StartRunning(Agent agent)
        {
            base.StartRunning(agent);

            centre = agent.Position + agent.transform.forward * -centreDistance;
        }

        public override void Process(Agent agent)
        {
            base.Process(agent);

            TargetHolder targetHolder = agent.GetComponent<TargetHolder>();
            Vector3 targetPos = targetHolder.target.transform.position;

            Vector3 direction = (targetPos - centre).normalized;
            Vector3 cameraPos = centre + direction * centreDistance;
            moveTarget = cameraPos;

            Vector3 distVec = moveTarget - agent.Position;
            float distMagSq = distVec.sqrMagnitude;

            if (distMagSq > arriveDist * arriveDist)
            {
                float dist = Mathf.Sqrt(distMagSq);

                float thisMoveSpeed = moveSpeed * dist;
                float movement = Time.deltaTime * thisMoveSpeed;

                if (movement > dist)
                    movement = dist;

                Vector3 dir = distVec * (1f / dist);

                Vector3 movementVec = dir * movement;
                Vector3 newPos = agent.transform.position + movementVec;
                agent.transform.position = newPos;

                Vector3 lookDir = (newPos - centre).normalized;

                targetRot = Quaternion.LookRotation(lookDir) * Quaternion.Euler(rotationOffset);

            }

            float angle = Quaternion.Angle(targetRot, agent.transform.rotation);
            if (angle > arriveAngle)
            {
                float thisRotationSpeed = rotateSpeed * angle;
                float angleMovement = thisRotationSpeed * Time.deltaTime;
                if (angleMovement > angle)
                    angleMovement = angle;

                float t = angleMovement / angle;

                Quaternion final = Quaternion.Lerp(agent.transform.rotation, targetRot, t);
                agent.transform.rotation = final;
            }

        }
    }

}
