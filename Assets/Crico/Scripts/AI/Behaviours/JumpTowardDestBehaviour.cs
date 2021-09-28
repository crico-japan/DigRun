using UnityEngine;
using UnityEngine.AI;

namespace Crico.AI.Behaviours
{
    public class JumpTowardDestBehaviour : AgentBehaviour
    {
        [SerializeField] float jumpAngle = 45f;
        [SerializeField] float jumpSpeed = 10f;
        [SerializeField] float gravity = 10f;
        [SerializeField] float floorHeight = 0f;

        bool done;
        Vector3 vel;

        public override void StartRunning(Agent agent)
        {
            base.StartRunning(agent);

            Vector3 dir = (agent.Destination - agent.Position).normalized;
            Vector3 rotationAxis = Vector3.Cross(dir, Vector3.up);

            dir = Quaternion.AngleAxis(jumpAngle, rotationAxis) * dir;

            vel = dir * jumpSpeed;

            done = false;
        }

        public override void Process(Agent agent)
        {
            if (done)
                return;

            base.Process(agent);

            Vector3 accel = Vector3.down * gravity * Time.deltaTime;

            vel += accel * 0.5f;

            agent.Position += vel * Time.deltaTime;

            vel += accel * 0.5f;

            if (agent.Position.y <= floorHeight)
            {
                done = true;
            }
        }

        public override Status GetStatus()
        {
            if (done)
                return Status.SUCCESS;

            return base.GetStatus();
        }
    }

}
