using UnityEngine;

namespace Crico.AI.States
{
    public class StateTransitionOnTargetSpeed : StateTransition
    {
        [SerializeField] float speedThreshold = 0f;
        [SerializeField] bool greaterThan = false;

        public override bool CheckTransitionCondition(Agent agent)
        {
            TargetHolder targetHolder = agent.GetComponent<TargetHolder>();
            Rigidbody rigidbody = targetHolder.target.GetComponent<Rigidbody>();

            float speed = rigidbody.velocity.magnitude;

            bool result = speed <= speedThreshold;
            if (greaterThan)
                result = !result;

            return result;
        }
    }

}
