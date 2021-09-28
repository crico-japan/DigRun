using UnityEngine;
using UnityEngine.Assertions;

namespace Crico.AI.Behaviours
{
    public class SteeringBehaviour : AgentBehaviour
    {
        [SerializeField] float accel = 10f;
        [SerializeField] float slowDist = 3f;
        [SerializeField] float arriveDist = 1f;
        [SerializeField] float maxSpeed = 20f;

        [SerializeField] bool scaleByAnalogInputMag = false;
        [SerializeField] bool avoidSurroundings = false;
        [SerializeField] TriggerEventRelay surroundingsCollider;
        [SerializeField] float avoidSurroundingsStrengthMulti = 1f;
        [SerializeField] bool avoidObstaclesAhead = false;
        [SerializeField] TriggerEventRelay lookAheadCollider;
        [SerializeField] string[] lookAheadTagsToIgnore = new string[]{};

        [SerializeField] bool refreshDestFromTarget = false;

        Vector3 avoidanceSteering = Vector3.zero;
        Agent agent;

        private float lastPol = 0f;

        private void AssertInspectorVars()
        {
            if (avoidSurroundings)
                Assert.IsNotNull(surroundingsCollider);

            if (avoidObstaclesAhead)
            {
                Assert.IsNotNull(lookAheadCollider);
                Assert.IsNotNull(lookAheadCollider.GetComponent<BoxCollider>());

            }
        }

        private void AddEventListenersToRelaysAndDisable()
        {
            surroundingsCollider?.gameObject.SetActive(false);
            if (avoidSurroundings)
            {
                surroundingsCollider.onTriggerStay.AddListener(OnSurroundingsTriggerStay);
            }

            lookAheadCollider?.gameObject.SetActive(false);
            if (avoidObstaclesAhead)
            {
                lookAheadCollider.onTriggerStay.AddListener(OnLookAheadTriggerStay);
            }
        }

        private void Awake()
        {
            AssertInspectorVars();
            AddEventListenersToRelaysAndDisable();
        }

        public override void StartRunning(Agent agent)
        {
            base.StartRunning(agent);

            this.agent = agent;

            if (avoidSurroundings)
                surroundingsCollider.gameObject.SetActive(true);

            if (avoidObstaclesAhead)
                lookAheadCollider.gameObject.SetActive(true);
        }

        public override void StopRunning()
        {
            base.StopRunning();

            if (avoidSurroundings)
                surroundingsCollider.gameObject.SetActive(false);

            if (avoidObstaclesAhead)
                lookAheadCollider.gameObject.SetActive(false);

        }


        private void FixedUpdate()
        {
            avoidanceSteering = Vector3.zero;
        }

        private void OnLookAheadTriggerStay(TriggerEventRelay relay, Collider collider)
        {
            if (collider.gameObject == agent.gameObject)
                return;

            foreach (string tag in lookAheadTagsToIgnore)
                if (tag == collider.tag)
                    return;

            Vector3 nearestPoint = collider.ClosestPoint(agent.transform.position);

            Vector3 distVec = nearestPoint - agent.Position;
            distVec.y = 0f;

            float dotRight = Vector3.Dot(agent.transform.right, distVec);

            float polarity = 1f;
            if (dotRight > 0f)
            {
                // left side
                polarity = -polarity;
            }

            float fwdDist = Vector3.Project(distVec, agent.transform.forward).magnitude;

            float maxDist = ((BoxCollider)relay.collider).size.z;

            float distanceAttenuation = (maxDist - fwdDist) / maxDist;

            if (distanceAttenuation <= 0f)
            {
                return;
            }

            float strength = polarity * distanceAttenuation * accel;

            Vector3 steering = strength * agent.transform.right;
            avoidanceSteering += steering;
        }

        private void OnSurroundingsTriggerStay(TriggerEventRelay relay, Collider collider)
        {
            if (collider.gameObject == agent.gameObject)
                return;

            Rigidbody rigidbody = collider.attachedRigidbody;
            if (rigidbody != null && rigidbody.velocity.sqrMagnitude == 0f)
                return;

            Vector3 nearestPoint = collider.ClosestPoint(relay.transform.position);

            Vector3 distVec = (relay.transform.position - nearestPoint);
            distVec.y = 0f;

            float distMag = distVec.magnitude;

            float avoidRadius = relay.collider.bounds.extents.x;
            float strength = avoidSurroundingsStrengthMulti * accel * (avoidRadius - distMag) / avoidRadius;

            Vector3 steering = strength * distVec.normalized;
            avoidanceSteering += steering;
        }

        public override void Process(Agent agent)
        {
            base.Process(agent);

            if (refreshDestFromTarget)
            {
                agent.Destination = agent.GetComponent<TargetHolder>().target.transform.position;
            }

            Vector3 distVec = agent.Destination - agent.Position;
            distVec.y = 0f;

            Vector3 dirVec = distVec.normalized;

            float dist = distVec.magnitude;
            if (dist <= arriveDist)
            {
                agent.Rigidbody.AddForce(-agent.Rigidbody.velocity, ForceMode.VelocityChange);
                return;
            }

            float currentMaxSpeed = this.maxSpeed;
            if (scaleByAnalogInputMag)
            {
                AnalogInputReceiver analogInputReceiver = agent.GetComponent<AnalogInputReceiver>();
                currentMaxSpeed *= analogInputReceiver.normalizedMag;
            }

            float targetSpeed = currentMaxSpeed;

            if (dist < slowDist)
                targetSpeed = currentMaxSpeed * dist / slowDist;

            Vector3 targetVel = targetSpeed * dirVec;

            Vector3 steering = targetVel - agent.Rigidbody.velocity;

            steering += avoidanceSteering;

            float maxAccel = Time.deltaTime * accel;


            if (steering.magnitude > maxAccel)
                steering = steering.normalized * maxAccel;

            agent.Rigidbody.AddForce(steering, ForceMode.VelocityChange);

            Vector3 agentVelocity = agent.Rigidbody.velocity;
            float amountToSlowBy = agentVelocity.magnitude - currentMaxSpeed;

            if (amountToSlowBy > 0f)
            {
                Vector3 slowVec = -agentVelocity.normalized * amountToSlowBy;
                agent.Rigidbody.AddForce(slowVec, ForceMode.VelocityChange);
            }

        }

     }

}
