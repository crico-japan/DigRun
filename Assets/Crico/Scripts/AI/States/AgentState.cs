using Crico.AI.Behaviours;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Crico.AI.States
{
    public class AgentState : State
    {
        [Header("Agent State Vars")]
        [SerializeField] List<AgentBehaviour> behaviours = new List<AgentBehaviour>();
        [SerializeField] bool setAnimState;
        [SerializeField] int animStateValue;
        [SerializeField] bool setNavMeshStopped;
        [SerializeField] bool navMeshStoppedValue;

        protected override void Awake()
        {
            base.Awake();

            Assert.IsNotNull(behaviours);
            foreach (AgentBehaviour behaviour in behaviours)
                Assert.IsNotNull(behaviour);
        }

        public override void Enter(Agent agent)
        {
            base.Enter(agent);

            if (setAnimState)
                agent.SetAnimationState(animStateValue);

            if (setNavMeshStopped)
                agent.SetNavMeshAgentStopped(navMeshStoppedValue);

            foreach (AgentBehaviour behaviour in behaviours)
                behaviour.StartRunning(agent);
        }

        public override void Process(Agent agent)
        {
            base.Process(agent);

            foreach (AgentBehaviour behaviour in behaviours)
                if (behaviour.gameObject.activeSelf)
                    behaviour.Process(agent);
        }

        public override void Exit(Agent agent)
        {
            base.Exit(agent);

            foreach (AgentBehaviour behaviour in behaviours)
                behaviour.StopRunning();
        }
    }

}
