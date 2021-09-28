using UnityEngine;

namespace Crico.AI
{
    [System.Serializable]
    public class AgentPropertyFloat : AgentProperty<float>
    {
        public AgentPropertyFloat(string name, float property)
        {
            this.name = name;
            this.property = property;
        }

    }

}
