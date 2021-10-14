using Crico;
using Crico.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(RandomIdGenerator))]
public class RockHitDamage : MonoBehaviour
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
    
    public void SetDisable()
    {
        disabled = true;
    }


    private void OnEnable()
    {
        damageId = GetComponent<RandomIdGenerator>().GenerateId();
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (disabled)
        {
            return;
        }

        DamageLocation location = collision.gameObject.GetComponent<DamageLocation>();
        if (location == null)
        {
            return;
        }

        if (collision.gameObject.transform.position.y > transform.position.y)
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
        if (damageTaken)
        {
            onSuccessfulHit.Invoke();
        }
    }
}
