using UnityEngine;

namespace Crico.AI.States
{
    public class StateTransitionOnDragInput : StateTransition
    {
        InputReceiver agentInputReceiver;
        bool inputReceived;

        public override void OnStateEnter(Agent agent)
        {
            base.OnStateEnter(agent);

            inputReceived = false;
            agentInputReceiver = agent.GetComponent<InputReceiver>();
            agentInputReceiver.onDrag.AddListener(OnDrag);
        }

        private void OnDrag(Vector2 input)
        {
            agentInputReceiver = null;
            inputReceived = true;
        }

        public override bool CheckTransitionCondition(Agent agent)
        {
            return inputReceived;
        }

    }

}
