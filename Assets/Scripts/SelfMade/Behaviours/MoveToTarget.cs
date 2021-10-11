using Crico.AI;
using Crico.AI.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : AgentBehaviour
{
    [SerializeField] float arriveTolerance = 0.01f;
    [SerializeField] float speed = 1f;
    [SerializeField] Plane plane = Plane.XY;
    
    enum Plane
    {
        XZ,
        XY,
    }

    public override void StopRunning()
    {
        base.StopRunning();
    }

    public override void Process(Agent agent)
    {
        base.Process(agent);
    }
}
