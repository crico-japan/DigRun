using UnityEngine;

namespace Crico.AI.States
{
    public class StateTransitionNEGATE : StateTransition
    {
        [SerializeField] StateTransition transition = null;

        public override bool CheckTransitionCondition(Agent agent)
        {
            return !transition.CheckTransitionCondition(agent);
        }
    }

}
