using UnityEngine;
using UnityEngine.Events;

namespace Crico
{
    public class InputReceiver : MonoBehaviour
    {
        public TouchSensor.PointerEvent onDrag { get; private set; } = new TouchSensor.PointerEvent();
        public TouchSensor.PointerEvent onPointerDown { get; private set; } = new TouchSensor.PointerEvent();
        public UnityEvent onPointerUp { get; private set; } = new UnityEvent();

        public void OnDrag(Vector2 input)
        {
            onDrag.Invoke(input);
        }

        public void OnPointerDown(Vector2 input)
        {
            onPointerDown.Invoke(input);
        }

        public void OnPointerUp()
        {
            onPointerUp.Invoke();
        }

    }

}
