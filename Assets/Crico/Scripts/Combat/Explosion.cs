using Crico.GameEvents;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Crico.Combat
{
    [RequireComponent(typeof(RandomIdGenerator))]
    public class Explosion : MonoBehaviour
    {
        [SerializeField] GameEventVector3 effectCreator;
        [SerializeField] int damageAmount;
        [SerializeField] DamageType damageType;
        [SerializeField] LayerMask applyForceMask;
        [SerializeField] float explosionForce;
        [SerializeField] float radius;
        [SerializeField] int damageSourceId;
     
        bool damageApplied;
        bool impulseApplied;
        bool secondFrame;

        public void SetDamageSourceId(int id)
        {
            this.damageSourceId = id;
        }

        private void Awake()
        {
            Assert.IsNotNull(effectCreator);
        }

        private void Start()
        {
            effectCreator.Raise(transform.position);
        }

        private void FixedUpdate()
        {
            if (secondFrame)
            {
                if (!impulseApplied)
                    ApplyImpulse();
            }
            else
            {
                if (!damageApplied)
                    ApplyDamage();
            }
        }

        private Rigidbody FindRootInRagdoll(Rigidbody limbBody)
        {
            Transform parent = limbBody.transform;

            while (
                parent != null
                && (parent.GetComponent<Rigidbody>() == null || parent.GetComponent<CharacterJoint>() != null)
                )
            {
                parent = parent.parent;
            }

            return parent.GetComponent<Rigidbody>();
        }

        private void ApplyImpulse()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius, applyForceMask.value);

            List<Rigidbody> alreadyApplied = new List<Rigidbody>();

            foreach (Collider collider in colliders)
            {
                Rigidbody rigidbody = collider.GetComponent<Rigidbody>();

                if (rigidbody == null)
                    continue;

                CharacterJoint characterJoint = collider.GetComponent<CharacterJoint>();

                if (characterJoint != null)
                {
                    Rigidbody ragdollRoot = FindRootInRagdoll(rigidbody);
                    if (ragdollRoot == null)
                        continue;

                    rigidbody = ragdollRoot;
                }

                if (alreadyApplied.Contains(rigidbody))
                    continue;

                Vector3 explosionForceVector = (rigidbody.position - transform.position).normalized * explosionForce;
                rigidbody.AddForce(explosionForceVector, ForceMode.Impulse);

                alreadyApplied.Add(rigidbody);
            }

            impulseApplied = true;
        }

        private void ApplyDamage()
        {
            int damageId = GetComponent<RandomIdGenerator>().GenerateId();

            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider collider in colliders)
            {
                DamageLocation damageLocation = collider.GetComponent<DamageLocation>();
                if (damageLocation != null)
                    damageLocation.TakeDamage(new DamagePacket(damageId, damageType, damageAmount, collider.ClosestPoint(transform.position), damageSourceId));

                /*
                SignalReceiver signalReceiver = collider.GetComponent<SignalReceiver>();
                if (signalReceiver != null)
                    signalReceiver.PersistSignalOn(signalToTransmit);*/
            }

            damageApplied = true;
        }

        private void Update()
        {
            if (secondFrame && impulseApplied)
            {
                Destroy(gameObject);
            }
            else if (damageApplied)
            {
                secondFrame = true;
            }
        }
    }

}

