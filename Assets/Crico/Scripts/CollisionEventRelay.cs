using UnityEngine;
using UnityEngine.Events;

namespace Crico
{
    [DisallowMultipleComponent, RequireComponent(typeof(Collider))]
    public class CollisionEventRelay : MonoBehaviour
    {
        [System.Serializable]
        public class CollisionEvent : UnityEvent<CollisionEventRelay, Collision> { };

        [SerializeField] CollisionEvent _onCollisionEnter = new CollisionEvent();
        [SerializeField] CollisionEvent _onCollisionStay = new CollisionEvent();
        [SerializeField] CollisionEvent _onCollisionExit = new CollisionEvent();

        public CollisionEvent onCollisionEnter { get => _onCollisionEnter; }
        public CollisionEvent onCollisionStay { get => _onCollisionStay; }
        public CollisionEvent onCollisionExit { get => _onCollisionExit; }

        private void OnCollisionEnter(Collision collision)
        {
            _onCollisionEnter.Invoke(this, collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            _onCollisionStay.Invoke(this, collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            _onCollisionExit.Invoke(this, collision);
        }

    }

}
