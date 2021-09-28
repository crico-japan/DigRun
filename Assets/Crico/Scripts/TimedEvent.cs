using UnityEngine;
using UnityEngine.Events;

namespace Crico
{
    public class TimedEvent : MonoBehaviour
    {
        [SerializeField] float timeUntilDeactivate;
        [SerializeField] UnityEvent eventToInvoke = new UnityEvent();

        float currentTime;

        private void OnEnable()
        {
            currentTime = timeUntilDeactivate;
        }

        private void Update()
        {
            if (currentTime <= 0f)
                return;

            currentTime -= Time.deltaTime;
            if (currentTime <= 0f)
                eventToInvoke.Invoke();
        }
    }

}
