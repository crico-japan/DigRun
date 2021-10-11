using Crico.AI;
using Crico.AI.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTransitionOnTriggerEnter : StateTransition
{
    [SerializeField] float arriveRange = 0.01f;
    [SerializeField] Plane plane = Plane.XY;
    enum Plane
    {
        XZ,
        XY,
    }

    bool doTransition = false;
    public override void OnStateEnter(Agent agent)
    {
        base.OnStateEnter(agent);
        doTransition = false;
    }
    public override bool CheckTransitionCondition(Agent agent)
    {
        //if(!doTransition)
        //{
        //    Vector3 pos = agent.transform.position;
        //    Vector3 dest = agent.GetComponent<TargetHolder>().target.transform.position;

        //    switch(plane)
        //    {
        //        case Plane.XY:
        //            dest.z = pos.z;
        //            break;
        //        case Plane.XZ:
        //            dest.y = pos.y;
        //            break;
        //    }

        //    float dist = (dest - pos).magnitude;
        //    doTransition = dist <= arriveRange;
        //}

        return doTransition;
    }

    private void OnTriggerStay(Collider other)
    {
        if (doTransition == true)
        {
            return;
        }

        if (other.gameObject.layer == LayerName.Character)
        {
            doTransition = true;
        }
    }
}
