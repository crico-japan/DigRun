using UnityEngine;

namespace Crico
{
    public class GroundedChecker : MonoBehaviour
    {
        [SerializeField] Vector3 rayOriginOffset = Vector3.zero;
        [SerializeField] float rayLength = 1f;

        public bool isGrounded { get; private set; }

        private void FixedUpdate()
        {
            Vector3 rayOrigin = transform.position + rayOriginOffset;
            Vector3 rayDirection = Vector3.down;

            isGrounded = Physics.Raycast(rayOrigin, rayDirection, rayLength);
        }

    }

}
