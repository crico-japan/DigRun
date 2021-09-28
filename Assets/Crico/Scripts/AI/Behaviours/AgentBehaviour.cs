using UnityEngine;

namespace Crico.AI.Behaviours
{
    public abstract class AgentBehaviour : MonoBehaviour
    {
        public enum Status
        {
            INVALID,
            RUNNING,
            SUCCESS,
            FAILURE
        }

        public virtual Status GetStatus() { return Status.RUNNING; }
        public virtual void StartRunning(Agent agent) { }
        public virtual void StopRunning() { }
        public virtual void Process(Agent agent) { }
    }

}
