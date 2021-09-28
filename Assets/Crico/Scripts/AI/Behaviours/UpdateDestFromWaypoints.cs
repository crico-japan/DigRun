using UnityEngine;

namespace Crico.AI.Behaviours
{
    public class UpdateDestFromWaypoints : AgentBehaviour
    {
        [SerializeField] float arriveDist = 1f;
        [SerializeField] float finalWaypointXZJitterRadius = 1f;

        bool running = false;
        int currentIndex = -1;
        int numWaypoints;

        public override void StartRunning(Agent agent)
        {
            base.StartRunning(agent);

            if (currentIndex >= 0)
            {
                RefreshDestination(agent);
                return;
            }

            WaypointListHolder waypointListHolder = agent.GetComponent<WaypointListHolder>();
            numWaypoints = waypointListHolder.GetNumWaypoints();

            float distSq = float.MaxValue;
            int closest = -1;
            for (int i = 0; i < numWaypoints; ++i)
            {
                Vector3 waypointPos = waypointListHolder.GetWaypointPos(i);

                Vector3 dist = agent.Position - waypointPos;
                dist.y = 0f;
                float currentDistSq = dist.sqrMagnitude;
                if (currentDistSq < distSq)
                {
                    distSq = currentDistSq;
                    closest = i;
                }
            }

            currentIndex = closest;

            SetNextWaypoint(agent);
        }

        private bool CheckFinal()
        {
            return currentIndex == numWaypoints - 1;
        }

        private void RefreshDestination(Agent agent)
        {
            Vector3 destination = agent.GetComponent<WaypointListHolder>().GetWaypointPos(currentIndex);

            bool final = CheckFinal();
            if (final)
            {
                float randomAngle = Random.Range(-180f, 180f);

                float randomRadius = Random.Range(0f, finalWaypointXZJitterRadius);

                if (randomRadius > 0f)
                {
                    Vector3 toRotate = new Vector3(0f, 0f, randomRadius);
                    Quaternion rotator = Quaternion.Euler(0f, randomAngle, 0f);
                    destination += rotator * toRotate;
                }
            }

            agent.Destination = destination;
        }

        private void SetNextWaypoint(Agent agent)
        {
            if (currentIndex + 1 < numWaypoints)
            {
                ++currentIndex;
                RefreshDestination(agent);
                running = true;
            }
            else
            {
                running = false;
            }
        }


        public override void Process(Agent agent)
        {
            base.Process(agent);

            if (!running)
                return;

            WaypointListHolder waypointListHolder = agent.GetComponent<WaypointListHolder>();

            Vector3 waypointPos = waypointListHolder.GetWaypointPos(currentIndex);

            Vector3 dist = agent.Position - waypointPos;
            dist.y = 0f;

            if (dist.sqrMagnitude <= arriveDist * arriveDist)
            {
                SetNextWaypoint(agent);
            }
        }

    }

}
