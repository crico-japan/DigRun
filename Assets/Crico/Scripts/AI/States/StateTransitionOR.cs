using UnityEngine;

namespace Crico.AI.States
{
    public class StateTransitionOR : StateTransition
    {
        [SerializeField] StateTransition[] transitions = new StateTransition[0];

        public override bool CheckTransitionCondition(Agent agent)
        {
            if (transitions.Length == 0)
                return false;

            bool result = false;

            for (int i = 0; i < transitions.Length; ++i)
            {
                bool current = transitions[i].CheckTransitionCondition(agent);
                if (current)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
    }

}
