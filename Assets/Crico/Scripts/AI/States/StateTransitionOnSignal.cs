using UnityEngine;

namespace Crico.AI.States
{
    public class StateTransitionOnSignal : StateTransition
    {
        [SerializeField] float timeDelay;
        [SerializeField] SignalType signalType;
        [SerializeField] bool expectedValue;

        float timeLeft;

        public override void OnStateEnter(Agent agent)
        {
            base.OnStateEnter(agent);

            timeLeft = timeDelay;
        }

        public override bool CheckTransitionCondition(Agent agent)
        {
            bool result = false;

            SignalReceiver signalReceiver = agent.GetComponent<SignalReceiver>();
            if (signalReceiver.IsSignalOn(signalType) == expectedValue)
            {
                timeLeft -= Time.deltaTime;
                result = timeLeft <= 0f;
            }
            else
            {
                timeLeft = timeDelay;
            }

            return result;
            
        }
    }

}
