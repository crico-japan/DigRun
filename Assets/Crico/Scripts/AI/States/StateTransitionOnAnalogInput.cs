using UnityEngine;

namespace Crico.AI.States
{
    public class StateTransitionOnAnalogInput : StateTransition
    {
        [SerializeField] float magnitudeThreshold = 0f;
        [SerializeField] bool greaterThan = false;

        public override bool CheckTransitionCondition(Agent agent)
        {
            AnalogInputReceiver analogInputReceiver = agent.GetComponent<AnalogInputReceiver>();
            float magnitude = analogInputReceiver.normalizedMag;

            bool result = magnitude <= magnitudeThreshold;
            if (greaterThan)
                result = !result;

            return result;
        }
    }

}
