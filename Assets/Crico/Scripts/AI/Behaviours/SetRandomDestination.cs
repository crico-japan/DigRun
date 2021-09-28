using UnityEngine;
using UnityEngine.AI;

namespace Crico.AI.Behaviours
{
    public class SetRandomDestination : AgentBehaviour
    {
        const int MAX_SAMPLE_TRIES = 30;

        [SerializeField] float minRadius = 0f;
        [SerializeField] float maxRadius = 1f;
        [SerializeField] bool confineToNavMesh = false;
        [SerializeField] float navMeshSampleRange = 1f;

        public override void StartRunning(Agent agent)
        {
            base.StartRunning(agent);

            Vector3 agentLocation = agent.transform.position;

            float randomAngle = Random.Range(0f, 360f);

            Vector3 forward = agent.transform.forward;

            forward = Quaternion.Euler(0f, randomAngle, 0f) * forward;

            float dist = Random.Range(minRadius, maxRadius);

            Vector3 dest = agentLocation + forward * dist;

            if (confineToNavMesh)
            {
                Vector3 navMeshDest;
                bool result = GetPositionOnNavMesh(dest, navMeshSampleRange, out navMeshDest);

                if (result)
                {
                    dest = navMeshDest;
                }
                else
                {
                    dest = agentLocation;
                }
            }

            agent.Destination = dest;

        }

        private bool GetPositionOnNavMesh(Vector3 center, float range, out Vector3 result)
        {
            for (int i = 0; i < MAX_SAMPLE_TRIES; i++)
            {
                Vector3 randomPoint = center + Random.insideUnitSphere * range;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
                {
                    result = hit.position;
                    return true;
                }
            }

            result = Vector3.zero;
            return false;
        }
    }

}
