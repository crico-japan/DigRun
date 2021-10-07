using Crico.AI;
using Crico.AI.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTransitionOnScreen : StateTransition
{
    public override bool CheckTransitionCondition(Agent agent)
    {
        return true;
    }
}
