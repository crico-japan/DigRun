using System.Collections.Generic;
using UnityEngine;

namespace Crico.AI
{
    public class AgentList : MonoBehaviour
    {
        [SerializeField] List<Agent> agents;

        private void Reset()
        {
            agents = new List<Agent>();
            foreach (Transform child in transform)
            {
                Agent agent = child.GetComponent<Agent>();
                if (agent != null)
                {
                    agents.Add(agent);
                }
            }
        }

        public List<Agent> GetList()
        {
            return agents;
        }
    }

}
