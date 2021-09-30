using Crico.AI;
using Crico.AI.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBehaviour : AgentBehaviour
{
    [SerializeField]
    float angelDest = 90.0f;

    [SerializeField]
    float rotationTime = 1.0f;
    Agent agent;

    float dist = 0.0f;
    float speed;
    Vector3 axis;
    private void Awake()
    {
        speed = angelDest / rotationTime;
    }
    public override void StartRunning(Agent agent)
    {
        base.StartRunning(agent);
        this.agent = agent;
        agent.transform.rotation = Quaternion.identity;
    }

    public override void StopRunning()
    {
        base.StopRunning();
    }

    public override void Process(Agent agent)
    {
        base.Process(agent);

        if(dist >= angelDest)
        {
            return;
        }
        Quaternion q = Quaternion.AngleAxis(speed*Time.deltaTime, agent.transform.up);
        agent.transform.rotation *= q;
        dist += speed*Time.deltaTime;
    }
}
