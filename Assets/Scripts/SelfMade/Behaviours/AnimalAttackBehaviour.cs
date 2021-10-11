using Crico.AI;
using Crico.AI.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAttackBehaviour : AgentBehaviour
{
    private Status status = Status.INVALID;

    public override Status GetStatus()
    {
        return status;
    }
    public override void StartRunning(Agent agent)
    {
        base.StartRunning(agent);
        status = Status.RUNNING;
    }

    public override void StopRunning()
    {
        base.StopRunning();
        status = Status.INVALID;
    }

    public void SetSuccess()
    {
        status = Status.SUCCESS;
    }
}
