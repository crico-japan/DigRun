using UnityEngine;
using UnityEngine.Events;

namespace Crico.Combat
{
    [RequireComponent(typeof(RandomIdGenerator))]
    public class MeleeHitDamager : MonoBehaviour
    {
        [SerializeField] DamageType damageType = DamageType.NONE;
        [SerializeField] int damageAmount = 0;
        [SerializeField] float forceAmount = 0f;
        [SerializeField] bool overrideForceDirection = false;
        [SerializeField] Vector3 forceDirection = Vector3.zero;
        [SerializeField] UnityEvent onSuccessfulHit = new UnityEvent();

        int damageId;
        bool damageDealt = false;
        bool disabled;

        public void SetDisabled()
        {
            disabled = true;
        }

        private void OnEnable()
        {
            damageId = GetComponent<RandomIdGenerator>().GenerateId();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (disabled)
                return;

            DamageLocation location = other.GetComponent<DamageLocation>();
            if (location == null)
                return;

            Vector3 forceApplied = Vector3.zero;

            if (overrideForceDirection && forceDirection.sqrMagnitude > 0f)
            {
                forceApplied = forceDirection.normalized * forceAmount;
            }
            else
            {
                forceApplied = ((other.ClosestPoint(transform.position) - transform.position).normalized * forceAmount);
            }

            DamagePacket p = new DamagePacket();
            p.location = transform.position;
            p.amount = damageAmount;
            p.damageId = damageId;
            p.forceApplied = forceApplied;
            p.rigidBody = other.attachedRigidbody;
            p.sourceId = -1;
            p.type = damageType;

            bool damageTaken = location.TakeDamage(p);

            if (damageTaken)
                onSuccessfulHit.Invoke();
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnTriggerEnter(collision.collider);
        }

    }

}
