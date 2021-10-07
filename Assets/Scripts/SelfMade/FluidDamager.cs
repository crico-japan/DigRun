using Crico;
using Crico.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(RandomIdGenerator))]
public class FluidDamager : MonoBehaviour
{
    [SerializeField]
    Obi.ObiCollider obiCollider = null;

    [SerializeField]
    Obi.ObiSolver solver = null;

    [SerializeField]
    DamageLocation location = null;

    HashSet<int> hitParticle = new HashSet<int>();

    [SerializeField]
    int threshold = 5;

    [SerializeField]
    DamageType damageType = DamageType.NONE;

    [SerializeField]
    int damageAmount = 0;

    [SerializeField]
    float forceAmount = 0f;

    [SerializeField]
    bool overrideForceDirection = false;

    [SerializeField]
    Vector3 forceDirection = Vector3.zero;

    [SerializeField]
    UnityEvent onSuccessfulHit = new UnityEvent();

    int damageId;
    bool damageDealt = false;
    bool disabled;

    Obi.ObiColliderWorld world;
    public void SetDisabled()
    {
        disabled = true;
    }

    private void OnEnable()
    {
        damageId = GetComponent<RandomIdGenerator>().GenerateId();
    }

    private void Awake()
    {
        world = Obi.ObiColliderWorld.GetInstance();
        if(solver != null)
        {
            solver.OnCollision += Solver_OnCollision;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool isDrown = false;

    private void CheckDrown(int hitParticle)
    {
        int completion = Mathf.CeilToInt(hitParticle / 1000.0f * 100);
        Debug.Log(completion.ToString());
        if (completion > threshold)
        {
            if (isDrown == false)
            {
                isDrown = true;
                DamagePacket p = new DamagePacket();
                p.location = transform.position;
                p.amount = 1;
                p.damageId = GetComponent<RandomIdGenerator>().GenerateId();
                p.type = DamageType.Fluid;

                bool damageTaken = location.TakeDamage(p);
                if(damageTaken)
                {
                    onSuccessfulHit.Invoke();
                }
            }
        }
    }

    private void Solver_OnCollision(Obi.ObiSolver solver, Obi.ObiSolver.ObiCollisionEventArgs eventArgs)
    {
        foreach (Oni.Contact contact in eventArgs.contacts)
        {
            var col = world.colliderHandles[contact.bodyB].owner;
            if(contact.distance < 0.1f)
            {
                if (obiCollider == col)
                {
                    if (hitParticle.Add(contact.bodyA))
                    {
                        CheckDrown(hitParticle.Count);
                        Debug.Log("Particle Hit");
                    }
                }
            }
        }
    }
}
