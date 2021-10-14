using Crico.AI;
using Crico.AI.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScrollBehaviour : AgentBehaviour
{
    [SerializeField] float speed = 1.0f;
    [SerializeField] Transform target;
    Status status = Status.INVALID;

    public override Status GetStatus()
    {
        return status;
    }

    public override void StartRunning(Agent agent)
    {
        base.StartRunning(agent);
        target = agent.GetComponent<TargetHolder>().target.transform;
        var targetVP = Camera.main.WorldToViewportPoint(target.position);
        var camerVP = Camera.main.WorldToViewportPoint(agent.transform.position);
    }

    public override void StopRunning()
    {
        base.StopRunning();
        status = Status.INVALID;
    }

    public override void Process(Agent agent)
    {
        base.Process(agent);

        var targetViewportPos = Camera.main.WorldToViewportPoint(target.position);

        if(targetViewportPos.x <= 0.2f)
        {
            status = Status.SUCCESS;
            return;
        }

        agent.transform.position = new Vector3(speed * Time.deltaTime + agent.transform.position.x, agent.transform.position.y, agent.transform.position.z); 
    }
}
