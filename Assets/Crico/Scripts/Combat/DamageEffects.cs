using Crico.GameEvents;
using System.Collections.Generic;
using UnityEngine;

namespace Crico.Combat
{
    public class DamageEffects : MonoBehaviour
    {
        [System.Serializable]
        public class DamageEffectType
        {
            public DamageType type;
            public GameEventVector3 noDamageSpawnEvent;
            public GameEventVector3 damageSpawnEvent;

            public GameEventVector3 GetEvent(int damage)
            {
                if (damage > 0)
                    return damageSpawnEvent;
                else
                    return noDamageSpawnEvent;
            }
        }

        [SerializeField] private DamageEffectType defaultType = null;
        [SerializeField] private List<DamageEffectType> effectTypes = null;

        public void SpawnEffect(DamageType type, int amount, Vector3 location)
        {
            DamageEffectType effectType = FindEffectType(type);
            GameEventVector3 damageEvent = effectType.GetEvent(amount);
            if (damageEvent != null)
                damageEvent.Raise(location);
        }

        private DamageEffectType FindEffectType(DamageType type)
        {
            DamageEffectType effectType = effectTypes.Find(x => (x.type & type) != 0);

            if (effectType == null)
                effectType = this.defaultType;

            return effectType;
        }

    }

}
