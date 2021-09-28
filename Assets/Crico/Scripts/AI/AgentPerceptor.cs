using Crico.Combat;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Crico.AI
{
    [RequireComponent(typeof(Agent))]
    public class AgentPerceptor : MonoBehaviour
    {
        [SerializeField] float perceptionRange = 10f;
        [SerializeField] AgentList agentList;

        private void Awake()
        {
            Assert.IsNotNull(agentList);
        }

        public Agent FindClosestLivingTarget(Vector3 toLocation)
        {
            Agent self = GetComponent<Agent>();

            Agent result = null;
            float prevSqDist = float.MaxValue;

            List<Agent> agents = agentList.GetList();
            foreach (Agent agent in agents)
            {
                if (agent == self)
                    continue;

                if (!agent.isActiveAndEnabled)
                    continue;

                HPHolder hPHolder = agent.GetComponent<HPHolder>();
                if (hPHolder == null || hPHolder.IsDead)
                    continue;

                Transform agentTransform = agent.transform;
                Vector3 agentPos = agentTransform.position;
                float squareDist = (agentPos - toLocation).sqrMagnitude;
                if (squareDist <= perceptionRange * perceptionRange
                    && (result == null || squareDist <= prevSqDist))
                {
                    result = agent;
                    prevSqDist = squareDist;
                }
            }

            return result;
        }
    }

}
