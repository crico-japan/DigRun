using Crico.AI;
using Crico.AI.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveToTarget : AgentBehaviour
{
    [SerializeField] Transform destination;
    [SerializeField] float arriveTolerance = 0.01f;
    [SerializeField] float speed = 1f;
    [SerializeField] Plane plane = Plane.XY;

    Status status = Status.INVALID;
    enum Plane
    {
        XZ,
        XY,
    }

    public override Status GetStatus()
    {
        return status;
    }

    public override void StartRunning(Agent agent)
    {
        base.StartRunning(agent);
        agent.transform.DOMoveZ(destination.position.z, 0.5f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            status = Status.SUCCESS;
        });
    }

    public override void StopRunning()
    {
        base.StopRunning();
    }

    //public override void Process(Agent agent)
    //{
    //    base.Process(agent);

    //    Vector3 dest = destination.position;
    //    Vector3 position = agent.transform.position;

    //    switch(plane)
    //    {
    //        case Plane.XY:
    //            dest.z = position.z;
    //            break;

    //        case Plane.XZ:
    //            dest.y = position.y;
    //            break;
    //    }

    //    Vector3 dist = dest - position;
    //    float distMag = dist.magnitude;

    //    if(distMag <= arriveTolerance)
    //    {
    //        return;
    //    }

    //    Vector3 dir = dist.normalized;
    //    float movementMag = speed * Time.deltaTime;
    //    if(movementMag > distMag)
    //    {
    //        movementMag = distMag;
    //    }

    //    Vector3 movement = dir * movementMag;
    //    Vector3 newPos = position + movement;
    //    agent.transform.position = newPos;
    //}
}
