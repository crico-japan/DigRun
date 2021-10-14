using Crico.AI;
using Crico.AI.Behaviours;
using Crico.AI.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCameraStateBenaviour : AgentBehaviour
{
    [SerializeField] StateMachine targetStateMachine;

    Status status = Status.INVALID;

    public override Status GetStatus()
    {
        return status;
    }

    public override void StartRunning(Agent agent)
    {
        base.StartRunning(agent);
        targetStateMachine = GameObject.FindGameObjectWithTag(TagName.CameraStateMachine).GetComponent<StateMachine>();
    }

    public override void StopRunning()
    {
        base.StopRunning();
        status = Status.INVALID;
    }

    public override void Process(Agent agent)
    {
        base.Process(agent);

        if(targetStateMachine.GetCurrentStateName() == "Idle")
        {
            status = Status.SUCCESS;
        }
    }
}
