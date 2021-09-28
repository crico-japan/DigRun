using UnityEngine;
using UnityEngine.Events;

namespace Crico.AI.Behaviours
{
    public class InputDirectorBehaviour : AgentBehaviour
    {
        [SerializeField] TouchSensor.PointerEvent onDrag = new TouchSensor.PointerEvent();
        [SerializeField] TouchSensor.PointerEvent onPointerDown = new TouchSensor.PointerEvent();
        [SerializeField] UnityEvent onPointerUp = new UnityEvent();

        InputReceiver agentInputReceiver = null;
        public override void StartRunning(Agent agent)
        {
            base.StartRunning(agent);

            agentInputReceiver = agent.GetComponent<InputReceiver>();
            agentInputReceiver.onDrag.AddListener(onDrag.Invoke);
            agentInputReceiver.onPointerDown.AddListener(onPointerDown.Invoke);
            agentInputReceiver.onPointerUp.AddListener(onPointerUp.Invoke);
        }

        public override void StopRunning()
        {
            base.StopRunning();

            onPointerUp.Invoke();

            agentInputReceiver.onDrag.RemoveListener(onDrag.Invoke);
            agentInputReceiver.onPointerDown.RemoveListener(onPointerDown.Invoke);
            agentInputReceiver.onPointerUp.RemoveListener(onPointerUp.Invoke);
            agentInputReceiver = null;
        }
    }

}
