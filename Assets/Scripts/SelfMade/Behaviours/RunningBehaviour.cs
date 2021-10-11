using Crico.AI;
using Crico.AI.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningBehaviour : AgentBehaviour
{
    enum Direction
    {
        Right,
        Left,
    }
    [SerializeField] Direction direction = Direction.Left;
    [SerializeField] float moveSpeed = 1.0f;

    Agent agent;
    bool isRunning = false;
    public override void StartRunning(Agent agent)
    {
        base.StartRunning(agent);
        this.agent = agent;
        isRunning = true;
    }

    public override void StopRunning()
    {
        base.StopRunning();
        isRunning = false;
    }
    private void FixedUpdate()
    {
        if(isRunning == false)
        {
            return;
        }
        Vector3 dir = new Vector3();
        if(direction == Direction.Left)
        {
            dir = Vector3.left;
        }
        else
        {
            dir = Vector3.right;
        }
        agent.transform.position += dir * moveSpeed * Time.fixedDeltaTime;
    }
}
