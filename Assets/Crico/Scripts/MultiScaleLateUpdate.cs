using UnityEngine;

namespace Crico
{
    public class MultiScaleLateUpdate : MonoBehaviour
    {
        [SerializeField] float multiplier = 1f;

        private void LateUpdate()
        {
            Vector3 localScale = transform.localScale;
            localScale *= multiplier;

            if (!float.IsInfinity(localScale.sqrMagnitude))
                transform.localScale = localScale;
        }
    }

}
