using UnityEngine;
using UnityEngine.Events;

namespace Crico
{
    public class CollisionEnterEvent : MonoBehaviour
    {
        [SerializeField] string tagToReactTo = default;
        [SerializeField] UnityEvent onCollisionEnter = new UnityEvent();

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag != tagToReactTo)
                return;

            onCollisionEnter.Invoke();
        }
    }

}
