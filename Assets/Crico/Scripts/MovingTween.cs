using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Crico
{
    public class MovingTween : MonoBehaviour
    {
        float duration;
        float time;
        Func<float, float> easeFunc;
        Transform target; 
        Transform origin; 
        Transform dest;
        Action onFinished;

        bool running = false;

        public void Init(Transform target, Transform origin, Transform dest, float duration, Func<float, float> easeFunc, Action onFinished)
        {
            Assert.IsNotNull(target);
            Assert.IsNotNull(origin);
            Assert.IsNotNull(dest);
            Assert.IsTrue(duration > 0f);
            Assert.IsNotNull(easeFunc);

            time = 0f;
            this.duration = duration;
            this.easeFunc = easeFunc;
            running = true;
            this.target = target;
            this.origin = origin;
            this.dest = dest;
            this.onFinished = onFinished;

            UpdateTween(time);
        }

        private void UpdateTween(float time)
        {
            float t = time / duration;

            float adjustedT = easeFunc(t);

            Vector3 newPos = Vector3.Lerp(origin.position, dest.position, adjustedT);

            target.position = newPos;

            Quaternion newRot = Quaternion.Lerp(origin.rotation, dest.rotation, adjustedT);

            target.rotation = newRot;
        }

        private void Update()
        {
            if (!running)
                return;

            time += Time.deltaTime;
            if (time >= duration)
            {
                running = false;
                time = duration;
            }

            UpdateTween(time);

            if (!running)
            {
                if (onFinished != null)
                    onFinished();

                Destroy(this);
            }
        }

        public static float EaseOutCubic(float x)
        {
            return 1f - Mathf.Pow(1f - x, 3f);
        }
    }

}
