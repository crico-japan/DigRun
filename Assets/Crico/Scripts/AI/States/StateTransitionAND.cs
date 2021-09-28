using UnityEngine;

namespace Crico.AI.States
{
    public class StateTransitionAND : StateTransition
    {
        [SerializeField] StateTransition[] transitions = new StateTransition[0];


        public override bool CheckTransitionCondition(Agent agent)
        {
            if (transitions.Length == 0)
                return false;

            bool failure = false;

            for (int i = 0; i < transitions.Length; ++i)
            {
                bool current = transitions[i].CheckTransitionCondition(agent);
                if (!current)
                {
                    failure = true;
                    break;
                }
            }

            bool result = !failure;

            return result;
        }
    }

}
