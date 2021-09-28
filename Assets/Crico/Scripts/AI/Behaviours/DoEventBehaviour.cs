using UnityEngine;
using UnityEngine.Events;

namespace Crico.AI.Behaviours
{
    public class DoEventBehaviour : AgentBehaviour
    {
        [SerializeField] UnityEvent onStartRunning = new UnityEvent();
        [SerializeField] float timeUntilFinish = 0f;

        float time;

        public override void StartRunning(Agent agent)
        {
            base.StartRunning(agent);

            onStartRunning.Invoke();
            time = 0f;
        }

        public override void Process(Agent agent)
        {
            base.Process(agent);

            time += Time.deltaTime;
        }

        public override Status GetStatus()
        {
            Status result = Status.RUNNING;
            if (time >= timeUntilFinish)
                result = Status.SUCCESS;

            return result;
        }
    }

}
