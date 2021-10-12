using Crico.AI;
using Crico.AI.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcWalkDistBehaviour : AgentBehaviour
{
    [SerializeField] float moveDist = 0.5f;

    private float endPos = 0.0f;
    private Camera camera;
    private Status status = Status.INVALID;
    private void Awake()
    {
        camera = Camera.main;
    }
    public override Status GetStatus()
    {
        return status;
    }
    public override void StartRunning(Agent agent)
    {
        base.StartRunning(agent);
        var startPos = camera.WorldToViewportPoint(agent.transform.position);
        endPos = startPos.x + moveDist;
        status = Status.RUNNING;
    }

    public override void Process(Agent agent)
    {
        base.Process(agent);
        var currentPos = camera.WorldToViewportPoint(agent.transform.position);
        if(currentPos.x > 0.8f)
        {
            status = Status.SUCCESS;
        }
    }
}
