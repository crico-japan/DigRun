using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Crico
{
    public class PointerDownButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] UnityEvent onPointerDown = new UnityEvent();

        public void OnPointerDown(PointerEventData e)
        {
            onPointerDown.Invoke();
        }

    }

}
