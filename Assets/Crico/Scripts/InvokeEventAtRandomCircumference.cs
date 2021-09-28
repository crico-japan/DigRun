using Crico.GameEvents;
using UnityEngine;
using UnityEngine.Assertions;

namespace Crico
{
    public class InvokeEventAtRandomCircumference : MonoBehaviour
    {
        [SerializeField] float circleRadius = 8f;
        [SerializeField] GameEventVector3 eventToInvoke;

        private void Awake()
        {
            Assert.IsNotNull(eventToInvoke);
        }

        public void DoInvoke()
        {
            Vector3 position = Vector3.forward * circleRadius;
            float randomAngle = Random.Range(0f, 360f);
            position = Quaternion.Euler(0f, randomAngle, 0f) * position;
            position += transform.position;
            eventToInvoke.Raise(position);
        }
    }

}
