using Crico.AI;
using Crico.AI.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckOnScreenBehaviour : AgentBehaviour
{
    [SerializeField]
    [Range(0, 0.5f)]
    float widthMargin = 0.1f;

    [SerializeField]
    [Range(0, 0.5f)]
    float heightMargin = 0.1f;

    [SerializeField]
    float activateDist = 0.1f;

    private Agent agent;
    private Camera camera;
    private Rect rect = new Rect(0, 0, 1, 1);
    private Transform player;
    private void Awake()
    {
        camera = Camera.main;
        rect = new Rect(widthMargin, heightMargin, 1 - widthMargin*2, 1 - heightMargin*2);
        player = GameObject.Find("Player").transform;
    }

    public override void StartRunning(Agent agent)
    {
        base.StartRunning(agent);
        this.agent = agent;
    }
    public override void Process(Agent agent)
    {
        base.Process(agent);

        Trigger trigger = agent.GetComponent<Trigger>();

        if(CheckInScreen(agent.transform))
        {
            trigger.SetTrigger();
        }
    }

    public override void StopRunning()
    {
        base.StopRunning();
    }

    bool CheckInScreen(Transform transform)
    {
        var viewportPos = camera.WorldToViewportPoint(transform.position);
        var playerViewPortPos = camera.WorldToViewportPoint(player.position);
        //if(viewportPos.x < 1-0.25f)
        //{
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}
        if(Mathf.Abs(playerViewPortPos.x - viewportPos.x) < activateDist)
        {
            return true;
        }
        else
        {
            return false;
        }
        //if(rect.Contains(viewportPos))
        //{
        //    ShowText("‰æ–Ê“à");
        //    return true;
        //}
        //else
        //{
        //    ShowText("‰æ–ÊŠO");
        //    return false;
        //}
    }

    [SerializeField]
    Text text;
    void ShowText(string message)
    {
        if(text != null)
        {
            text.text = message;
        }
    }
}
