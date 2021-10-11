using Crico.AI;
using Crico.AI.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchCharacterBehaviour : AgentBehaviour
{
    [SerializeField] float rayLength = 3.0f;

    [SerializeField] Transform[] rayStarts;

    private int layerMask = 1 << LayerName.Character;

    private Status status = Status.INVALID;

    public override Status GetStatus()
    {
        return status;
    }
    public override void StartRunning(Agent agent)
    {
        base.StartRunning(agent);
        status = Status.RUNNING;
    }

    public override void StopRunning()
    {
        base.StopRunning();
        status = Status.INVALID;
    }

    public override void Process(Agent agent)
    {
        base.Process(agent);

        foreach(var obj in rayStarts)
        {
            RaycastHit hit;
            if (Physics.Raycast(obj.position, Vector3.left, out hit, rayLength, layerMask))
            {
                agent.GetComponent<TargetHolder>().target = hit.collider.gameObject;
                status = Status.SUCCESS;
            }
        }
    }

    public void OnDrawGizmos()
    {
        foreach(var point in rayStarts)
        {
            Gizmos.DrawLine(point.position, point.position + (Vector3.left * rayLength));
        }
    }
}
