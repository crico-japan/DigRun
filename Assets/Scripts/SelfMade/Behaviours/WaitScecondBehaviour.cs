using Crico.AI;
using Crico.AI.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitScecondBehaviour : AgentBehaviour
{
    [SerializeField]
    float waitScecound = 1.0f;

    Status status = Status.INVALID;

    Coroutine coroutine;
    public override Status GetStatus()
    {
        return status;
    }

    public override void StartRunning(Agent agent)
    {
        base.StartRunning(agent);
        status = Status.INVALID;
        coroutine = StartCoroutine(WaitSecond());
    }

    public override void StopRunning()
    {
        base.StopRunning();
        StopCoroutine(coroutine);
    }

    IEnumerator WaitSecond()
    {
        yield return new WaitForSeconds(waitScecound);

        status = Status.SUCCESS;
    }
}
