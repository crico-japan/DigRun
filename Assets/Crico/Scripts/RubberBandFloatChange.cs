using UnityEngine;
using UnityEngine.Events;

namespace Crico
{
    public class RubberBandFloatChange : MonoBehaviour
    {
        [SerializeField] float initial = 0f;
        [SerializeField] float speed = 10f;
        [SerializeField] float arriveTolerance = 0.001f;
        [SerializeField] UnityEvent<float> setter = new UnityEvent<float>();

        float current;
        float target;

        private void Awake()
        {
            current = initial;
            target = current;
        }

        public void SetTarget(float target)
        {
            this.target = target;
        }

        private void Update()
        {
            float dist = Mathf.Abs(target - current);
            if (dist <= arriveTolerance)
                return;

            float currentSpeed = dist * speed;

            float dir = (target - current) / dist;

            float amountToAdd = Time.deltaTime * currentSpeed * dir;

            current += amountToAdd;

            if (current < target)
            {
                if (current + amountToAdd > target)
                    current = target;
                else
                    current += amountToAdd;
            }
            else
            {
                if (current + amountToAdd < target)
                    current = target;
                else
                    current += amountToAdd;

            }

            setter.Invoke(current);

        }
    }

}
