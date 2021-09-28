using UnityEngine;

namespace Crico
{
    [RequireComponent(typeof(TrailRenderer), typeof(RectTransform))]
    public class FigureEightTrail : MonoBehaviour
    {
        [SerializeField] Vector2 offset = Vector2.zero;
        [SerializeField] float ax = 0.05f;
        [SerializeField] float ay = 0.08f;
        [SerializeField] float startTime = 2f;
        [SerializeField] float speed = 3f;
        [SerializeField] float zRot = 0f;

        TrailRenderer trailRenderer;

        Vector3 centrePos;
        float t = 0f;

        private void Awake()
        {
            trailRenderer = GetComponent<TrailRenderer>();
        }

        void Start()
        {
            centrePos = GetComponent<RectTransform>().anchoredPosition;
            trailRenderer.time = startTime;

            Update();
        }

        void Update()
        {
            Vector3 position = new Vector3(ax * Mathf.Sin(t * 2f), ay * Mathf.Sin(t));
            position = Quaternion.Euler(0f, 0f, zRot) * position;
            position += centrePos;
            position.x += offset.x;
            position.y += offset.y;

            GetComponent<RectTransform>().anchoredPosition = position;

            t += Time.deltaTime * speed;
        }

        private void OnEnable()
        {
            trailRenderer.time = startTime;
        }

    }

}
