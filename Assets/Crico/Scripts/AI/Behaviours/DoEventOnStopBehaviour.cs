using UnityEngine;
using UnityEngine.Events;

namespace Crico.AI.Behaviours
{
    public class DoEventOnStopBehaviour : AgentBehaviour
    {
        [SerializeField] UnityEvent onStopped = new UnityEvent();


        public override void StopRunning()
        {
            base.StopRunning();

            onStopped.Invoke();
        }
    }

}
