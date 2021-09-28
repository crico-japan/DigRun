using UnityEngine;

namespace Crico.AI
{
    public class ActiveAgentStatusChecker : MonoBehaviour
    {
        [SerializeField] Transform agentsParent = null;

        private void Reset()
        {
            agentsParent = transform;
        }

        public int CountNumActiveAgents()
        {
            int result = 0;

            foreach (Transform child in agentsParent)
            {
                AgentStatus status = child.GetComponent<AgentStatus>();
                if (status == null)
                    continue;

                bool active = CheckActive(status);

                if (active)
                {
                    ++result;
                }
            }

            return result;
        }

        private bool CheckActive(AgentStatus status)
        {
            bool result = status.IsAgentActive();

            return result;
        }

        public bool AreAnyAgentsActive()
        {
            bool result = false;

            foreach (Transform child in agentsParent)
            {
                AgentStatus status = child.GetComponent<AgentStatus>();
                if (status == null)
                    continue;

                bool active = CheckActive(status);

                if (active)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

    }

}
