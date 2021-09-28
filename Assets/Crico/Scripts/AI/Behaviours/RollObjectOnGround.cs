using UnityEngine;

namespace Crico.AI.Behaviours
{
    public class RollObjectOnGround : AgentBehaviour
    {
        [SerializeField] float radius = 1f;
        [SerializeField] float movementThreshold = 0.001f;
        public override void Process(Agent agent)
        {
            base.Process(agent);

            GroundedChecker checker = agent.GetComponent<GroundedChecker>();
            if (!checker.isGrounded)
                return;

            Rigidbody body = agent.GetComponent<Rigidbody>();

            Vector3 movement = body.velocity * Time.deltaTime;
            float distance = movement.magnitude;

            if (distance < movementThreshold)
                return;

            float angle = distance * (180f / Mathf.PI) / radius;
            Vector3 rotationAxis = Vector3.Cross(Vector3.up, movement).normalized;
            agent.transform.rotation = Quaternion.Euler(rotationAxis * angle) * agent.transform.rotation;
        }

    }

}
