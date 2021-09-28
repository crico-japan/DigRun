using UnityEngine;

namespace Crico.AI.States
{
    public class StateTransitionOnTimeOut : StateTransition
    {
        [SerializeField] float timeOutTimeMin = 3f;
        [SerializeField] float timeOutTimeMax = 4f;

        float entryTime = 0f;
        float timeOutTime = 0f;

        public override void OnStateEnter(Agent agent)
        {
            base.OnStateEnter(agent);

            entryTime = Time.time;
            timeOutTime = Random.Range(timeOutTimeMin, timeOutTimeMax);
        }


        public override bool CheckTransitionCondition(Agent agent)
        {
            return (Time.time - entryTime) >= timeOutTime;
        }



    }

}
