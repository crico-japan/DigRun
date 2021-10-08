using Crico;
using Crico.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(RandomIdGenerator))]
public class RangeHitDamager : MonoBehaviour
{
    [SerializeField] DamageType damageType = DamageType.ENEMY_RANGED;
    [SerializeField] int damageAmount = 0;
    [SerializeField] UnityEvent onSuccessfulHit = new UnityEvent();

    int damageId;
    bool damageDealt = false;

    private void OnEnable()
    {
        damageId = GetComponent<RandomIdGenerator>().GenerateId();
    }

    private void OnTriggerEnter(Collider other)
    {
        DamageLocation location = other.GetComponent<DamageLocation>();
        if(location == null)
        {
            return;
        }

        DamagePacket p = new DamagePacket();
        p.location = transform.position;
        p.amount = damageAmount;
        p.damageId = damageId;
        p.sourceId = -1;
        p.type = damageType;

        bool damageTaken = location.TakeDamage(p);
        if(damageTaken)
        {
            onSuccessfulHit.Invoke();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnTriggerEnter(collision.collider);
    }
}
