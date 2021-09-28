using UnityEngine;

namespace Crico.AI.States
{
    public class StateTransitionOnYPos : StateTransition
    {
        [SerializeField] float yLimit = -1f;
        public override bool CheckTransitionCondition(Agent agent)
        {
            return agent.Position.y < yLimit;
        }
    }

}
