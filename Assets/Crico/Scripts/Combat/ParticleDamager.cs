using UnityEngine;

namespace Crico.Combat
{
    [RequireComponent(typeof(RandomIdGenerator))]
    public class ParticleDamager : MonoBehaviour
    {
        [SerializeField] int damage = 1;
        [SerializeField] DamageType damageType = DamageType.PLAYER_MELEE;

        private void OnParticleCollision(GameObject other)
        {
            DamageLocation damageLocation = other.GetComponent<DamageLocation>();

            if (damageLocation == null)
                return;

            RandomIdGenerator randomIdGenerator = GetComponent<RandomIdGenerator>();
            int id = randomIdGenerator.GenerateId();

            damageLocation.TakeDamage(new DamagePacket(id, damageType, damage, other.transform.position));
        }

    }

}
