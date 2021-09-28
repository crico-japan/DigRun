using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crico.AI.Behaviours;
using Crico.AI;

public class WalkBehaviour : AgentBehaviour
{
    [SerializeField]
    float moveSpeed = 2.0f;

    Agent agent;
    private void Awake()
    {
        
    }

    public override void StartRunning(Agent agent)
    {
        base.StartRunning(agent);
        this.agent = agent;
        agent.Rigidbody.AddForce(agent.transform.right * moveSpeed, ForceMode.VelocityChange);
    }

    public override void StopRunning()
    {
        base.StopRunning();
        agent.Rigidbody.velocity = Vector3.zero;
    }

    public override void Process(Agent agent)
    {
        base.Process(agent);

    }
}
