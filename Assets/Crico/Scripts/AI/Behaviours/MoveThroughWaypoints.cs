using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

namespace Crico.AI.Behaviours
{
    public class MoveThroughWaypoints : AgentBehaviour
    {
        [SerializeField] bool useNavMesh = false;
        [SerializeField] float arriveDist = 0.1f;
        [SerializeField] float moveSpeed = 3f;

        int currentIndex = 0;
        int numWaypoints = 0;

        public override void StartRunning(Agent agent)
        {
            base.StartRunning(agent);

            WaypointListHolder holder = agent.GetComponent<WaypointListHolder>();
            Assert.IsNotNull(holder);

            numWaypoints = holder.GetNumWaypoints();
            agent.GetComponent<NavMeshAgent>().speed = moveSpeed;
            agent.Destination = GetCurrentWaypointPos(agent);
        }

        public override void Process(Agent agent)
        {
            base.Process(agent);

            bool arrived = CheckArrivedAtCurrentPoint(agent);

            if (arrived)
            {
                if (currentIndex + 1 < numWaypoints)
                {
                    ++currentIndex;
                    if (useNavMesh)
                    {
                        Vector3 newDest = GetCurrentWaypointPos(agent);
                        agent.SetNavMeshAgentDest(newDest);
                    }
                    agent.Destination = GetCurrentWaypointPos(agent);
                }
                else
                {
                    if (useNavMesh)
                        agent.SetNavMeshAgentStopped(true);
                }
            }
            else
            {
                if (!useNavMesh)
                {
                    DoManualMovement(agent);
                }
            }

            
        }

        private void DoManualMovement(Agent agent)
        {
            Vector3 currentTarget = GetCurrentWaypointPos(agent);
            Vector3 distVec = currentTarget - agent.transform.position;
            float dist = distVec.magnitude;
            float movement = moveSpeed * Time.deltaTime;

            if (movement > dist)
                movement = dist;

            Vector3 dir = distVec * (1f / dist);
            Vector3 movementVec = dir * movement;
            Vector3 newPos = agent.transform.position + movementVec;
            agent.transform.position = newPos;
        }

        private Vector3 GetCurrentWaypointPos(Agent agent)
        {
            return agent.GetComponent<WaypointListHolder>().GetWaypointPos(currentIndex);
        }

        private bool CheckArrivedAtCurrentPoint(Agent agent)
        {
            Vector3 currentPoint = GetCurrentWaypointPos(agent);
            bool result = (agent.transform.position - currentPoint).sqrMagnitude <= arriveDist * arriveDist;
            return result;
            
        }

    }

}
