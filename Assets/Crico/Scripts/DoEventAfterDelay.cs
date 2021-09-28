using UnityEngine;
using UnityEngine.Events;

namespace Crico
{
    public class DoEventAfterDelay : MonoBehaviour
    {
        [SerializeField] float delay;
        [SerializeField] UnityEvent todo = new UnityEvent();

        private void Start()
        {
            if (delay <= 0f)
                todo.Invoke();
        }

        private void Update()
        {
            if (delay > 0f)
                delay -= Time.deltaTime;

            if (delay <= 0f)
                todo.Invoke();
        }
    }

}
