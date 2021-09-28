using UnityEngine;

namespace Crico.AI.Behaviours
{
    public class FaceMoveDirBehaviour : FaceDirectionBehaviour
    {
        [SerializeField] bool xzOnly;
        Vector3 lastPos;
        Vector3 lastMoveDist;
        

        public override void StartRunning(Agent agent)
        {
            base.StartRunning(agent);

            lastPos = agent.transform.position;
        }

        public override Vector3 GetDestForward(Agent agent)
        {
            return lastMoveDist.normalized;
        }

        public override void Process(Agent agent)
        {
            Vector3 position = agent.transform.position;
            Vector3 distVec = position - lastPos;
            if (xzOnly)
                distVec.y = 0f;

            lastPos = position;
            if (distVec.sqrMagnitude > 0f)
            {
                lastMoveDist = distVec;

                base.Process(agent);
            }
        }
    }

}
