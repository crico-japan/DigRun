using Crico.AI;
using Crico.AI.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsCameraArrivedBehaviour : AgentBehaviour
{
    [SerializeField] float ArriveDist = 0.01f;
    [SerializeField] Agent agent;
    private Camera camera;
    private Status status = Status.INVALID;
    private Vector2 initViewportPos;

    Vector3 followDistVec;
    private void Awake()
    {
        camera = Camera.main;
        initViewportPos = camera.WorldToViewportPoint(agent.transform.position);

        followDistVec = camera.transform.position - agent.transform.position;
    }

    public override Status GetStatus()
    {
        return status;
    }

    public override void StartRunning(Agent agent)
    {
        base.StartRunning(agent);
        status = Status.RUNNING;
    }

    public override void Process(Agent agent)
    {
        base.Process(agent);

        var currentViewportPos = camera.WorldToViewportPoint(agent.transform.position);

        if (currentViewportPos.x <= 0.2f)
        {
            status = Status.SUCCESS;
        }

        Transform target = agent.transform;

    }
}
