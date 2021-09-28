using Crico.Combat;
using UnityEngine;

namespace Crico.AI.States
{
    public class StateTransitionOnAllHPZero : StateTransition
    {
        [SerializeField] Transform hpHoldersParent;

        public override bool CheckTransitionCondition(Agent agent)
        {
            bool oneStillAlive = false;
            foreach (Transform child in hpHoldersParent)
            {
                HPHolder holder = child.GetComponent<HPHolder>();
                if (holder == null)
                    continue;

                if (holder.hp > 0)
                {
                    oneStillAlive = true;
                    break;
                }
            }
            
            bool makeTransition = !oneStillAlive;

            return makeTransition;
        }

    }

}
