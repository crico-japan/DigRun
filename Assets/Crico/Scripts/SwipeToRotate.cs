using UnityEngine;
using UnityEngine.Assertions;

namespace Crico
{
    public class SwipeToRotate : MonoBehaviour
    {
        [SerializeField] Vector3 rotationPerUnitX = new Vector3(0f, 0.5f, 0f);
        [SerializeField] Vector3 rotationPerUnitY = new Vector3(0.5f, 0f, 0f);
        [SerializeField] float speed = 45f;
        [SerializeField] float stopRotationAngle = 1f;
        [SerializeField] Vector3 maxRotation = new Vector3(180f, 180f, 180f);
        [SerializeField] Vector3 minRotation = new Vector3(-180f, -180f, -180f);

        Vector3 cumulativeRotation = new Vector3();
        Quaternion originalLocalRot = Quaternion.identity;

        float lastTime;

        private void Awake()
        {
            originalLocalRot = transform.localRotation;
        }

        public void OnDrag(Vector2 dragDist)
        {
            Vector3 rotation = rotationPerUnitX * dragDist.x + rotationPerUnitY * dragDist.y;
            cumulativeRotation += rotation;

            cumulativeRotation.x = Mathf.Clamp(cumulativeRotation.x, minRotation.x, maxRotation.x);
            cumulativeRotation.y = Mathf.Clamp(cumulativeRotation.y, minRotation.y, maxRotation.y);
            cumulativeRotation.z = Mathf.Clamp(cumulativeRotation.z, minRotation.z, maxRotation.z);
        }

        private void FixedUpdate()
        {
            float time = Time.time;
            float delta = time - lastTime;
            lastTime = time;

            if (cumulativeRotation.sqrMagnitude == 0f)
                return;

            Quaternion targetRotation = Quaternion.Euler(cumulativeRotation) * originalLocalRot;

            float targetCurrentAngle = Quaternion.Angle(transform.localRotation, targetRotation);
            if (targetCurrentAngle > stopRotationAngle)
            {
                float deltaAngle = delta * speed;
                if (deltaAngle > targetCurrentAngle)
                    deltaAngle = targetCurrentAngle;

                float angleProp = deltaAngle / targetCurrentAngle;

                Quaternion newRotation = Quaternion.Lerp(transform.localRotation, targetRotation, angleProp);
                transform.localRotation = newRotation;
            }

        }
    }

}
