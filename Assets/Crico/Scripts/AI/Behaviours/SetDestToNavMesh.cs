using UnityEngine.AI;

namespace Crico.AI.Behaviours
{
    public class SetDestToNavMesh : AgentBehaviour
    {

        public override void StartRunning(Agent agent)
        {
            base.StartRunning(agent);

            agent.GetComponent<NavMeshAgent>().SetDestination(agent.Destination);
        }
    }

}
