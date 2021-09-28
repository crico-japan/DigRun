using UnityEngine;

namespace Crico.Combat
{
    public class DamageForceBuffer : MonoBehaviour
    {
        [SerializeField] Rigidbody optionalOverride = null;

        private Rigidbody objectToApplyForce;
        private Vector3 forceToApply;

        public void ReportDamageForce(Rigidbody objectEffected, Vector3 force)
        {
            objectToApplyForce = objectEffected;
            forceToApply = force;
        }

        public void ApplyForceAndFlush()
        {
            if (objectToApplyForce == null)
                return;

            if (optionalOverride != null)
                objectToApplyForce = optionalOverride;

            objectToApplyForce.AddForce(forceToApply, ForceMode.Impulse);
            objectToApplyForce = null;
            forceToApply = Vector3.zero;
        }
    }
}