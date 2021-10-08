using Crico.AI;
using Crico.AI.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockClimbBehaviour : AgentBehaviour
{
    private Agent agent;
    private Vector3 normal;
    public override void StartRunning(Agent agent)
    {
        base.StartRunning(agent);
        this.agent = agent;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == TagName.Rock)
        {
            Vector3 contactPoint = new Vector3();
            foreach (var contact in collision.contacts)
            {
                var normalBuf = contact.normal;
                normalBuf.z = 0;
                normal += normalBuf;
                contactPoint += contact.point;
            }

            contactPoint /= collision.contactCount;

            Quaternion q = Quaternion.FromToRotation(agent.transform.up, normal.normalized);
            agent.transform.rotation *= q;

            agent.transform.position = new Vector3(contactPoint.x, contactPoint.y, agent.transform.position.z);
        }
    }
}
