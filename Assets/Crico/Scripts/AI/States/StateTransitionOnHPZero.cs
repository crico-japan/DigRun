using Crico.Combat;
using UnityEngine;

namespace Crico.AI.States
{
    public class StateTransitionOnHPZero : StateTransition
    {
        public override bool CheckTransitionCondition(Agent agent)
        {
            HPHolder hpHolder = agent.GetComponent<HPHolder>();
            return hpHolder.hp <= 0;
        }

    }

}
