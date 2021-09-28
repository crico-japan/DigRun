using UnityEngine;

namespace Crico.Combat
{
    public class DamagePacket
    {
        public int damageId = -1;
        public DamageType type = DamageType.NONE;
        public int amount = 0;
        public Vector3 location = Vector3.zero;
        public int sourceId = -1;
        public Vector3 forceApplied = Vector3.zero;
        public Rigidbody rigidBody = null;

        public DamagePacket() { }

        public DamagePacket(int damageId, DamageType damageType, int damageAmount, Vector3 damageLocation)
        {
            this.damageId = damageId;
            this.type = damageType;
            this.amount = damageAmount;
            this.location = damageLocation;
        }

        public DamagePacket(int damageId, DamageType damageType, int damageAmount, Vector3 damageLocation, int damageSourceId)
        {
            this.damageId = damageId;
            this.type = damageType;
            this.amount = damageAmount;
            this.location = damageLocation;
            this.sourceId = damageSourceId;
        }
    }
}