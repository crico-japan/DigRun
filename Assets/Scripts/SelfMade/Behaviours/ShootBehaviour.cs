using Crico.AI;
using Crico.AI.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBehaviour : AgentBehaviour
{
    [SerializeField]
    GameObject bullet = null;

    [SerializeField]
    Transform bulletPos = null;

    private enum Direction
    {
        Left,
        Right
    }

    [SerializeField]
    Direction direction = Direction.Left;

    public override void StartRunning(Agent agent)
    {
        base.StartRunning(agent);
        //var obj = Instantiate(bullet, transform.position, Quaternion.identity);
        //obj.GetComponent<Bullet>().SetDirection(Vector3.left);
    }

    public override void Process(Agent agent)
    {
        base.Process(agent);

    }
}
