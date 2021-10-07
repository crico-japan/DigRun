using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crico.AI.Behaviours;
using Crico.AI;

public class WalkBehaviour : AgentBehaviour
{
    [SerializeField]
    float moveSpeed = 2.0f;

    [SerializeField]
    float gravityScale = 0.8f;

    [SerializeField]
    Transform left;

    [SerializeField]
    Transform right;

    [SerializeField]
    Transform buttom;

    [SerializeField]
    int numPoints = 10;

    [SerializeField]
    float rayLength = 0.5f;

    [SerializeField]
    float angelLimit = 60.0f;

    [SerializeField]
    Agent agent;

    private List<Transform> rayPoints = new List<Transform>();
    float width;
    float space;

    [SerializeField]
    Vector3 normal;

    bool isRunnning = false;
    int layerMask = 1 << LayerNameMask.GroundMask | 1 << LayerNameMask.RockMask;

    private void Awake()
    {
        width = Mathf.Abs(right.position.x - left.position.x);
        space = width / numPoints;
        for (int i = 0; i < numPoints; i++)
        {
            GameObject child = new GameObject();
            child.name = "RayPoint";
            child.transform.SetParent(transform);
            child.transform.position = new Vector3(left.position.x + (i * space), left.position.y, left.position.z);
            rayPoints.Add(child.transform);
        }
    }

    public override void StartRunning(Agent agent)
    {
        base.StartRunning(agent);
        this.agent = agent;
        //agent.Rigidbody.AddForce(agent.transform.right * moveSpeed, ForceMode.VelocityChange);
        isRunnning = true;
    }

    public override void StopRunning()
    {
        base.StopRunning();
        //agent.Rigidbody.velocity = Vector3.zero;
        isRunnning = false;
    }

    public override void Process(Agent agent)
    {
        base.Process(agent);
        normal = Vector3.up;

        RaycastHit hit;
        foreach (var point in rayPoints)
        {
            if (Physics.Raycast(point.position, -agent.transform.up.normalized, out hit, rayLength, layerMask))
            {
                var normalBuf = hit.normal;
                normalBuf.z = 0;
                normal += normalBuf;
            }
            else
            {
                normal += Vector3.up;
            }
        }
        agent.Rigidbody.velocity = Vector3.zero;
    }

    public void FixedUpdate()
    {
        if(isRunnning ==false)
        {
            return;
        }
        //ŒX‚«‚ð‹‚ß‚é
        Quaternion q = Quaternion.FromToRotation(agent.transform.up, normal.normalized);

        //‰ñ“]‚³‚¹‚é
        if(angelLimit > Vector3.Angle(Vector3.up, normal))
        {
            agent.transform.rotation *= q;
            agent.transform.position = agent.transform.position + (agent.transform.right * moveSpeed * Time.fixedDeltaTime);
        }


        RaycastHit hit;
        if (Physics.Raycast(buttom.position, -agent.transform.up, out hit, float.PositiveInfinity, layerMask))
        {
            if (hit.distance > 0.1f)
            {
                agent.transform.position = agent.transform.position + (-agent.transform.up * Physics.gravity.magnitude * gravityScale * Time.fixedDeltaTime);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var point in rayPoints)
        {
            Gizmos.DrawSphere(point.position, 0.1f);
            Gizmos.DrawLine(point.position, point.position + (-agent.transform.up * rayLength));
        }

        Gizmos.color = Color.black;
        Gizmos.DrawRay(agent.transform.position, agent.transform.position + (normal * 5));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(buttom.position, buttom.position + (agent.transform.up * 3));

        Gizmos.color = Color.blue;
        Vector3 u = new Vector3(normal.y, -normal.x);

    }
}
