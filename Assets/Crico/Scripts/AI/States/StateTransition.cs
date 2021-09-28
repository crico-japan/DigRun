using UnityEngine;

namespace Crico.AI.States
{
    public abstract class StateTransition : MonoBehaviour
    {
        [SerializeField] private State _destination;

        public State destination { get => _destination; }

        public virtual void OnStateEnter(Agent agent) { }
        public abstract bool CheckTransitionCondition(Agent agent);

    }

}
