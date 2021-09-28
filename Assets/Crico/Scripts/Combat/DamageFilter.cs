using UnityEngine;
using UnityEngine.Events;

namespace Crico.Combat
{
    [RequireComponent(typeof(HPHolder))]
    public class DamageFilter : MonoBehaviour
    {
        [System.Serializable]
        public class DamageEvent : UnityEvent<int> { }

        [SerializeField] DamageEvent ReportDamageSource = new DamageEvent();
        [SerializeField] private DamageType damageType = DamageType.NONE;

        private int lastDamageId = -1;

        public bool TakeDamage(DamagePacket p)
        {
            if (p.damageId == lastDamageId)
                return false;

            lastDamageId = p.damageId;

            if ((damageType & p.type) == 0)
                p.amount = 0;

            int actualDamageTaken = GetComponent<HPHolder>().TakeDamage(p.amount);

            DamageEffects damageEffects = GetComponent<DamageEffects>();
            if (damageEffects != null)
                damageEffects.SpawnEffect(p.type, p.amount, p.location);

            if (actualDamageTaken <= 0)
                return false;

            ReportDamageSource.Invoke(p.sourceId);

            DamageForceBuffer damageForceBuffer = GetComponent<DamageForceBuffer>();
            if (damageForceBuffer != null
                && p.rigidBody != null)
            {
                damageForceBuffer.ReportDamageForce(p.rigidBody, p.forceApplied);
            }

            return true;
        }
    }

}
