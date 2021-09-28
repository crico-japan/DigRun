using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Crico
{
    public class RandomTimedEvent : MonoBehaviour
    {
        [SerializeField] UnityEvent eventToInvoke = new UnityEvent();
        [SerializeField] bool invokeOnStart = false;
        [SerializeField] float minTime = 0f;
        [SerializeField] float maxTime = 1f;
        [SerializeField] int repeatCount = 0;

        float currentTime;

        private void Awake()
        {
            Assert.IsTrue(minTime > 0f);
            Assert.IsTrue(maxTime >= minTime);
            currentTime = 0f;
        }

        public void StartRunning()
        {
            if (invokeOnStart)
                eventToInvoke.Invoke();

            ResetTime();
        }

        private void ResetTime()
        {
            if (minTime == maxTime)
            {
                currentTime = minTime;
                return;
            }

            currentTime = Random.Range(minTime, maxTime);
        }


        private void Update()
        {
            if (currentTime > 0f)
            {
                currentTime -= Time.deltaTime;
                if (currentTime <= 0f)
                {
                    eventToInvoke.Invoke();

                    if (repeatCount > 0)
                    {
                        --repeatCount;
                    }

                    if (repeatCount != 0)
                    {
                        ResetTime();
                    }
                }
            }
        }
    }

}
