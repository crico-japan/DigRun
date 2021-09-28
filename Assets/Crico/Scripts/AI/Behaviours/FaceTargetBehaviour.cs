using UnityEngine;

namespace Crico.AI.Behaviours
{
    public class FaceTargetBehaviour : FaceDirectionBehaviour
    {
        public override Vector3 GetDestForward(Agent agent)
        {
            TargetHolder targetHolder = agent.GetComponent<TargetHolder>();
            Transform target = targetHolder.target.transform;

            return CalculateDestForward(agent, target.position);
        }
    }

}
