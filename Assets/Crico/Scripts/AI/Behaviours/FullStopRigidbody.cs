using UnityEngine;

namespace Crico.AI.Behaviours
{
    public class FullStopRigidbody : AgentBehaviour
    {
        [SerializeField] bool xzOnly = false;

        public override void StartRunning(Agent agent)
        {
            base.StartRunning(agent);

            Rigidbody rigidbody = agent.Rigidbody;

            Vector3 newVelocity = rigidbody.velocity;

            if (!xzOnly)
                newVelocity.y = 0f;

            newVelocity.x = 0f;
            newVelocity.z = 0f;

            rigidbody.velocity = newVelocity;
            rigidbody.angularVelocity = Vector3.zero; 
            rigidbody.ResetInertiaTensor();

        }
    }

}
