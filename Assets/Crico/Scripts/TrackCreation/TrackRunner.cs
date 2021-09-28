using UnityEngine;
using UnityEngine.Assertions;
using DG.Tweening;

namespace Crico.TrackCreation
{
    [RequireComponent(typeof(Rigidbody))]
    public class TrackRunner : MonoBehaviour
    {
        [SerializeField] private Track track = null;
        [SerializeField] private Transform modelParent = null;
        [SerializeField] private GameObject fastEffect = null;
        [SerializeField] private Animator animator = null;
        [SerializeField] private new Follower camera = null;
        [SerializeField] private string animTrigFastToSlow = "fastToSlow";
        [SerializeField] private string animTrigSlowToFast = "slowToFast";
        [SerializeField] private string animTrigAnyToDance = "anyToDance";
        [SerializeField] private string slipAnimStateName = "slip";
        [SerializeField] private float _hitExplosionForce = 500f;
        [SerializeField] private float _hitExplosionRadius = 1f;
        [SerializeField] private float speedLow = 1f;
        [SerializeField] private float baseAnimSpeed = 1f;
        [SerializeField] private float speedHigh = 5f;
        [SerializeField] private string slipZoneTag = "SlipZone";
        [SerializeField] private string fallZoneTag = "FallZone";
        [SerializeField] private float fallJump = 2f;
        [SerializeField] private float fallAngularSpeed = 360f;
        [SerializeField] private float turnTime = 0.3f;
        [SerializeField] private float cameraZoomAmount = 1f;
        [SerializeField] private float cameraZoomTime = 0.3f;


        public float hitExplosionRadius { get => _hitExplosionRadius; }
        public float hitExplosionForce { get => _hitExplosionForce; }
        public bool isAtHighSpeed { get; private set; }
        private float distMoved;
        private bool isFalling;
        private int slipZoneCount;
        private bool inFallZone;
        private Collider fallZone;
        private float modelOffset;

        private bool pastHighSpeed;
        private bool isDancing;

        private void AssertInspectorVars()
        {
            Assert.IsNotNull(track);
            Assert.IsNotNull(fastEffect);
            Assert.IsNotNull(animator);
            Assert.IsNotNull(camera);
        }

        private void Awake()
        {
            AssertInspectorVars();
            modelOffset = -modelParent.transform.localPosition.y;
            fastEffect.SetActive(false);
        }

        private void Start()
        {
            distMoved = 0f;
            RefreshPos();
        }

        private void RefreshPos()
        {
            Vector3 pos = track.GetPositionAndOrientation(distMoved, out Vector3 direction, out Vector3 normal);
            Vector3 up = Vector3.Cross(direction, normal);
            pos += modelOffset * up;
            transform.position = pos;

            transform.right = normal;

            transform.localRotation = Quaternion.LookRotation(direction, up);
        }

        public float GetSpeed()
        {
            float speed = isAtHighSpeed ? speedHigh : speedLow;
            return speed;
        }
        
        private void UpdateRunning()
        {
            float animSpeed = baseAnimSpeed;

            isAtHighSpeed = Input.GetMouseButton(0);

            if (isAtHighSpeed != pastHighSpeed)
            {
                pastHighSpeed = isAtHighSpeed;

                string trigger;

                if (isAtHighSpeed)
                {
                    trigger = animTrigSlowToFast;
                }
                else
                {
                    trigger = animTrigFastToSlow;
                }

                animator.SetTrigger(trigger);

                fastEffect.SetActive(isAtHighSpeed);
            }

            float speed = GetSpeed();
            animSpeed = speed * baseAnimSpeed;

            if (isAtHighSpeed && slipZoneCount > 0)
            {
                StartFalling();

                Vector3 direction = new Vector3();
                track.GetPositionAndOrientation(distMoved, out direction, out Vector3 normal);
                SetToFallingVelocity(direction);
            }
            else if (!isAtHighSpeed && inFallZone)
            {
                StartFalling();

                Vector3 fallZoneRight = fallZone.transform.right;
                Vector3 posDiff = transform.position - fallZone.transform.position;
                float dot = Vector3.Dot(fallZoneRight, posDiff);

                float multiplier = dot > 0f ? 1f : -1f;

                Vector3 direction = fallZoneRight * multiplier;
                direction.Normalize();
                SetToFallingVelocity(direction);
            }

            if (!isFalling)
            {
                animator.speed = animSpeed;
                distMoved += speed * Time.deltaTime;
                RefreshPos();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == slipZoneTag)
            {
                ++slipZoneCount;
            }
            else if (other.tag == fallZoneTag)
            {
                inFallZone = true;
                fallZone = other;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == slipZoneTag)
            {
                --slipZoneCount;
            }
            else if (other.tag == fallZoneTag)
            {
                inFallZone = false;
                fallZone = null;
            }
        }

        public void StartFallingBack(Vector3 fallDirection)
        {
            if (isFalling)
                return;

            StartFalling();
            SetToFallingVelocity(fallDirection);
        }

        private void SetToFallingVelocity(Vector3 direction)
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();

            Vector3 fallVelocity = speedHigh * direction;
            fallVelocity.y += fallJump;

            rigidbody.velocity = fallVelocity;
        }

        private void StartFalling()
        {
            if (isFalling)
                return;

            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;

            animator.speed = 1f;
            animator.Play(slipAnimStateName);

            float radians = fallAngularSpeed * Mathf.Deg2Rad;
            rigidbody.angularVelocity = new Vector3(0f, radians, 0f);

            isFalling = true;
            fastEffect.SetActive(false);
        }

        private void StartDancing()
        {
            isDancing = true;

            fastEffect.SetActive(false);

            Vector3 cameraPos = camera.transform.position;

            transform.DOLookAt(cameraPos, turnTime)
                .SetEase(Ease.OutCubic)
                .OnComplete(StartDanceAnim);

            camera.StopFollowing();

            Vector3 cameraForward = camera.transform.forward;
            cameraForward.Normalize();

            Vector3 destPos = camera.transform.position + cameraForward * cameraZoomAmount;
            camera.transform.DOMove(destPos, cameraZoomTime).SetEase(Ease.OutBounce);
        }

        private void StartDanceAnim()
        {
            animator.speed = 1f;
            animator.SetTrigger(animTrigAnyToDance);
        }

        private void Update()
        {
            if (!isFalling && !isDancing)
            {
                UpdateRunning();
                if (track.IsPlayerInEndZone())
                {
                    StartDancing();
                }
            }
        }

    }

}
