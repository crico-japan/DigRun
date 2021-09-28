using UnityEngine;

namespace Crico.AI.Behaviours
{
    public class MatchTargetVelocity : AgentBehaviour
    {
        [SerializeField] float speedRatio = 1f;

        public override void Process(Agent agent)
        {
            base.Process(agent);

            TargetHolder targetHolder = agent.GetComponent<TargetHolder>();
            GameObject target = targetHolder.target;
            Rigidbody rigidbody = target.GetComponent<Rigidbody>();

            Vector3 targetVel = rigidbody.velocity;

            Vector3 steering = targetVel * speedRatio - agent.Rigidbody.velocity;

            if (steering.sqrMagnitude == 0f)
                return;

            agent.Rigidbody.AddForce(steering, ForceMode.VelocityChange);
        }
    }

}
