using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Crico
{
    [RequireComponent(typeof(RectTransform))]
    public class TouchSensor : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [System.Serializable]
        public class PointerEvent : UnityEvent<Vector2> { }

        [SerializeField] PointerEvent _onDragInCanvasUnits = new PointerEvent();
        [SerializeField] PointerEvent _onPointerDown = new PointerEvent();
        [SerializeField] UnityEvent _onPointerUp = new UnityEvent();
        [SerializeField, Range(-1, 1)] int xMultiplier = 1;
        [SerializeField, Range(-1, 1)] int yMultiplier = -1;

        float dpc;
        Vector2 screenToCanvas;
        Vector2 lastDragPos;

        public PointerEvent onDragInCanvasUnits { get => _onDragInCanvasUnits; }
        public PointerEvent onPointerDown { get => _onPointerDown; }
        public UnityEvent onPointerUp { get => _onPointerUp; }


        private void Awake()
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            Rect rect = rectTransform.rect;
            screenToCanvas = new Vector2();

            screenToCanvas.x = rect.width / Screen.width;
            screenToCanvas.y = rect.height / Screen.height;
        }

        public void OnBeginDrag(PointerEventData pointerEventData)
        {
            lastDragPos = pointerEventData.position;
        }

        public void OnDrag(PointerEventData pointerEventData)
        {
            Vector2 currentDragPos = pointerEventData.position;

            Vector2 dragDistScreen = currentDragPos - lastDragPos;
            Vector2 dragDistCanvas = screenToCanvas * dragDistScreen;

            lastDragPos = currentDragPos;

            Vector2 multiplier = new Vector2((float)xMultiplier, (float)yMultiplier);
            Vector2 adjustedDragDist = dragDistCanvas * multiplier;
            onDragInCanvasUnits.Invoke(adjustedDragDist);
        }

        public void OnPointerDown(PointerEventData e)
        {
            Vector2 currentDragPos = e.position;
            Vector2 dragDistCanvas = screenToCanvas * currentDragPos;

            onPointerDown.Invoke(dragDistCanvas);
        }

        public void OnPointerUp(PointerEventData e)
        {
            onPointerUp.Invoke();
        }
    }

}
