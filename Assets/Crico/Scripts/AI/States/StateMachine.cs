using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace Crico.AI.States
{
    public class StateMachine : State
    {
        const string NULL_STATE_NAME = "Null";
        const int TRANSITION_LIMIT = 16;

        [Header("StateMachine")]
        [SerializeField] bool topLevel = true;
        [SerializeField] Agent agent;
        [SerializeField] State firstState = null;


        /*[SerializeField]*/ State currentState = null;

        private void Reset()
        {
            agent = GetComponentInParent<Agent>();
        }

        protected override void Awake()
        {
            base.Awake();
            Assert.IsNotNull(firstState);
            currentState = null;

            if (topLevel)
                Enter(agent);
        }

        public override void Enter(Agent agent)
        {
            base.Enter(agent);

            currentState = firstState;
            currentState.Enter(agent);
        }

        private void Update()
        {
            if (topLevel)
                Process(agent);
        }

        private State GetNextState()
        {
            return currentState.GetNextState(agent);
        }

        public override void Process(Agent agent)
        {
            base.Process(agent);

            int numTransitions = 0;

            currentState.Process(agent);
            State nextState = GetNextState();
            while (nextState != null && numTransitions < TRANSITION_LIMIT)
            {
                ++numTransitions;
                currentState.Exit(agent);
                nextState.Enter(agent);
                currentState = nextState;
                
                nextState = GetNextState();
            }

            if (numTransitions > TRANSITION_LIMIT)
                Debug.LogError("MAX TRANSITIONS EXCEEDED!");
        }

        public string GetCurrentStateName()
        {
            if (currentState == null)
                return NULL_STATE_NAME;

            return currentState.GetStateName();
        }

        public override string GetStateName()
        {
            return GetCurrentStateName();
        }


#if UNITY_EDITOR

        [CustomEditor(typeof(StateMachine)), CanEditMultipleObjects]
        public class StateMachineEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                foreach (Object target in targets)
                {
                    StateMachine stateMachine = (StateMachine)target;
                    State currentState = stateMachine.currentState;
                    string currentStateName = currentState != null ? currentState.gameObject.name : "Null";
                    EditorGUILayout.LabelField(string.Format("Current State: {0}", currentStateName));
                }


            }
        }
#endif



    }

}
