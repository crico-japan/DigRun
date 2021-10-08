using Crico.AI;
using Crico.AI.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCollisionBehaviour : AgentBehaviour
{
    public override void StartRunning(Agent agent)
    {
        base.StartRunning(agent);
        agent.gameObject.layer = LayerName.Cadaver;
    }
}
