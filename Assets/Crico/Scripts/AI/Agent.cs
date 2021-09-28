using UnityEngine;
using UnityEngine.AI;

namespace Crico.AI
{
    public class Agent : MonoBehaviour
    {
        [Header("Optional")]
        [SerializeField] Animator animator;
        [SerializeField] string animationStateParamName = "state";
        [SerializeField] NavMeshAgent navMeshAgent;
        [SerializeField] Vector3 destination;

        public Vector3 Destination { get => destination; set => destination = value; }
        public Vector3 Position { get => transform.position; set => transform.position = value; }
        public Rigidbody Rigidbody { get => GetComponent<Rigidbody>(); }


        public void SetAnimationState(int value)
        {
            if (animator == null)
                return;

            animator.SetInteger(animationStateParamName, value);
        }

        public void SetAnimationTrigger(string trigger)
        {
            if (animator == null)
                return;

            animator.SetTrigger(trigger);
        }

        public void SetNavMeshAgentStopped(bool value)
        {
            if (navMeshAgent == null || !navMeshAgent.enabled)
                return;

            navMeshAgent.isStopped = value;
        }

        public void SetNavMeshAgentDest(Vector3 dest)
        {
            if (navMeshAgent == null || !navMeshAgent.enabled)
                return;

            navMeshAgent.SetDestination(dest);
        }


        public bool IsAnimatorInState(string stateName)
        {
            if (animator == null)
                return true;

            return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
        }

    }

}
