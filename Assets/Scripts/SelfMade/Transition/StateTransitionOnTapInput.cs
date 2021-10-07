using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Crico.AI.States
{
    public class StateTransitionOnTapInput : StateTransition
    {
        InputReceiver agentInputReceiver;
        bool inputReceived;

        public override void OnStateEnter(Agent agent)
        {
            base.OnStateEnter(agent);
            inputReceived = false;
            agentInputReceiver = agent.GetComponent<InputReceiver>();
            agentInputReceiver.onPointerUp.AddListener(OnTap);
        }

        private void OnTap()
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

