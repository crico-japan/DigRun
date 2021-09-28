using UnityEngine;

namespace Crico.AI.Behaviours
{
    public abstract class FaceDirectionBehaviour : AgentBehaviour
    {
        [SerializeField] float arriveTolerance = 0.1f;
        [SerializeField] float rotationSpeed = 180f;

        abstract public Vector3 GetDestForward(Agent agent);

        public override void Process(Agent agent)
        {
            Vector3 destForward = GetDestForward(agent);

            if (destForward.sqrMagnitude == 0f)
                return;

            float angleDist = CalcAngleDist(agent, destForward);

            Quaternion destRot = Quaternion.LookRotation(destForward);

            if (angleDist > arriveTolerance)
            {
                float angleToMove = Time.deltaTime * rotationSpeed;

                if (angleToMove >= angleDist)
                {
                    angleToMove = angleDist;
                }

                float lerpTime = angleToMove / angleDist;

                Quaternion thisRot = Quaternion.Lerp(agent.transform.rotation, destRot, lerpTime);
                agent.transform.rotation = thisRot;
            }
        }

        private float CalcAngleDist(Agent agent, Vector3 destForward)
        {
            Vector3 currentForward = agent.transform.forward;

            float angleDist = Vector3.Angle(currentForward, destForward);

            return angleDist;
        }

        protected Vector3 CalculateDestForward(Agent agent, Vector3 locationToFace)
        {
            Vector3 pos = agent.transform.position;

            locationToFace.y = pos.y;

            if (locationToFace == pos)
                locationToFace = pos + agent.transform.forward * 0.1f;

            Vector3 destForward = (locationToFace - pos).normalized;

            return destForward;
        }
    }

}
