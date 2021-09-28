using UnityEngine;

namespace Crico.Combat
{
    [RequireComponent(typeof(RandomIdGenerator))]
    public class ContactDamager : MonoBehaviour
    {
        [SerializeField] float timeSpan = 0.333f;
        [SerializeField] int damagePerTimespan = 1;
        [SerializeField] DamageType damageType = DamageType.PLAYER_MELEE;

        float time = 0f;
        bool damageFrame = false;

        private void OnTriggerStay(Collider other)
        {
            if (!damageFrame)
                return;

            DamageLocation damLoc = other.GetComponent<DamageLocation>();
            if (damLoc == null)
                return;

            RandomIdGenerator idgen = GetComponent<RandomIdGenerator>();
            int damageId = idgen.GenerateId();
            Vector3 location = other.ClosestPoint(transform.position);

            damLoc.TakeDamage(new DamagePacket(damageId, damageType, damagePerTimespan, location));
        }

        private void FixedUpdate()
        {
            time += Time.fixedDeltaTime;
            if (time >= timeSpan)
            {
                damageFrame = true;
                time %= timeSpan;
            }
            else
            {
                damageFrame = false;
            }
        }
    }

}
