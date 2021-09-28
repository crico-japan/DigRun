using UnityEngine;
using UnityEngine.Assertions;

namespace Crico.Combat
{
    public class DamageLocation : MonoBehaviour
    {
        [SerializeField] int multiplier = 1;
        private DamageFilter damageFilter = null;

        private void Awake()
        {
            damageFilter = FindDamageFilter();
            Assert.IsNotNull(damageFilter);
        }

        private DamageFilter FindDamageFilter()
        {
            DamageFilter result = GetComponent<DamageFilter>();

            Transform parent = transform.parent;
            while (result == null && parent != null)
            {
                result = parent.GetComponent<DamageFilter>();
                parent = parent.parent;
            }

            return result;
        }

        public bool TakeDamage(DamagePacket p)
        {
            p.amount *= multiplier;
            return damageFilter.TakeDamage(p);
        }

    }

}
