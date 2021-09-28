using UnityEngine;
using UnityEngine.AI;

namespace Crico.AI.Behaviours
{
    public class SetDestAwayFromTarget : AgentBehaviour
    {
        [SerializeField] float minRotation = 45f;
        [SerializeField] float maxRotation = 60f;
        [SerializeField] float distAway = 10f;

        public override void StartRunning(Agent agent)
        {
            base.StartRunning(agent);

            Transform target = agent.GetComponent<TargetHolder>().target.transform;

            Vector3 dir = (agent.Position - target.position).normalized;

            float rotationAmount = Random.Range(minRotation, maxRotation);

            if (rotationAmount != 0f)
            {
                float angleToRotate = Random.Range(0, 2) == 0 ? rotationAmount : -rotationAmount;
                dir = Quaternion.Euler(0f, angleToRotate, 0f) * dir;
            }

            Vector3 newDest = target.position + dir * distAway;

            agent.Destination = newDest;
        }
    }

}
