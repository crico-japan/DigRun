using UnityEngine;

namespace Crico.AI
{
    [System.Serializable]
    public class AgentPropertyVec3 : AgentProperty<Vector3>
    {
        public AgentPropertyVec3(string name, Vector3 property)
        {
            this.name = name;
            this.property = property;
        }

    }

}
