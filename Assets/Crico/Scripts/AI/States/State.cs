using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Crico.AI.States
{
    public class State : MonoBehaviour
    {
        [Header("State")]
        [SerializeField] protected List<StateTransition> transitions = new List<StateTransition>();

        protected virtual void Awake()
        {
            Assert.IsNotNull(transitions);
            foreach (StateTransition transition in transitions)
                Assert.IsNotNull(transition);
        }

        public virtual void Enter(Agent agent)
        {
            foreach (StateTransition transition in transitions)
                transition.OnStateEnter(agent);
        }

        public virtual void Process(Agent agent) { }
        public virtual void Exit(Agent agent) { }

        public virtual State GetNextState(Agent agent)
        {
            foreach (StateTransition transition in transitions)
            {
                if (transition.CheckTransitionCondition(agent))
                    return transition.destination;
            }

            return null;
        }

        public virtual string GetStateName()
        {
            return gameObject.name;
        }

        protected void AddIfNotInTransitions(StateTransition stateTransition)
        {
            if (!transitions.Contains(stateTransition))
                transitions.Add(stateTransition);
        }
    }

}
