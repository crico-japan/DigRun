using UnityEngine;
using UnityEngine.Assertions;

namespace Crico.AI.Behaviours
{
    public class SequenceBehaviour : AgentBehaviour
    {
        [SerializeField] AgentBehaviour[] list = new AgentBehaviour[] { };

        bool running = false;
        int current;

        private void Reset()
        {
            AgentBehaviour[] prelist = GetComponentsInChildren<AgentBehaviour>();

            list = new AgentBehaviour[prelist.Length - 1];
            for (int i = 1; i < prelist.Length; ++i)
                list[i - 1] = prelist[i];
        }

        public override void StartRunning(Agent agent)
        {
            base.StartRunning(agent);
            Assert.IsTrue(list.Length > 0);

            current = 0;
            running = true;

            list[current].StartRunning(agent);
        }

        public override void Process(Agent agent)
        {
            base.Process(agent);

            if (!running)
                return;

            AgentBehaviour currentBehaviour = list[current];
            if (currentBehaviour.GetStatus() == Status.SUCCESS)
            {
                currentBehaviour.StopRunning();

                ++current;
                if (current < list.Length)
                {
                    list[current].StartRunning(agent);
                }
                else
                {
                    running = false;
                }
            }
            else
            {
                // RUNNING
                currentBehaviour.Process(agent);
            }
        }

        public override void StopRunning()
        {
            base.StopRunning();

            if (running)
                list[current].StopRunning();
        }

        public override Status GetStatus()
        {
            return running ? Status.RUNNING : Status.SUCCESS;
        }
    }

}
