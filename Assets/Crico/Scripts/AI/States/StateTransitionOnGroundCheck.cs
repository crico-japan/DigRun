using UnityEngine;

namespace Crico.AI.States
{
    public class StateTransitionOnGroundCheck : StateTransition
    {
        [SerializeField] float delayTime = 0f;
        [SerializeField] bool expectedValue = false;

        bool waitingForTimer = false;
        float waitStartTime;

        public override void OnStateEnter(Agent agent)
        {
            base.OnStateEnter(agent);

            waitingForTimer = false;
        }

        public override bool CheckTransitionCondition(Agent agent)
        {
            GroundedChecker checker = agent.GetComponent<GroundedChecker>();

            bool result = false;

            if (waitingForTimer)
            {
                if (checker.isGrounded == expectedValue)
                {
                    if (Time.time - waitStartTime >= delayTime)
                        result = true;
                }
                else
                {
                    waitingForTimer = false;
                }
            }
            else
            {
                if (checker.isGrounded == expectedValue)
                {
                    waitingForTimer = true;
                    waitStartTime = Time.time;
                    if (delayTime <= 0f)
                        result = true;
                }
                else
                {
                    // ‰½‚à‚µ‚È‚¢
                }
            }

            return result;
        }
    }

}
