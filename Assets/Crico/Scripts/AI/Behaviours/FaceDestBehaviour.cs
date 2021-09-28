using UnityEngine;

namespace Crico.AI.Behaviours
{
    public class FaceDestBehaviour : FaceDirectionBehaviour
    {
        public override Vector3 GetDestForward(Agent agent)
        {
            return CalculateDestForward(agent, agent.Destination);
        }
    }

}
