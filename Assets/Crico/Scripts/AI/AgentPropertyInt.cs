using UnityEngine;

namespace Crico.AI
{
    [System.Serializable]
    public class AgentPropertyInt : AgentProperty<int>
    {
        public AgentPropertyInt(string name, int property)
        {
            this.name = name;
            this.property = property;
        }

    }

}
