using UnityEngine;
using UnityEngine.Assertions;

namespace Crico
{
    public class Follower : MonoBehaviour
    {
        [SerializeField] private Transform target = null;
        [SerializeField] private float minY = 0f;
        
        private Vector3 targetPosDiff = new Vector3();
        private bool isFollowing;

        private void AssertInspectorVars()
        {
            Assert.IsNotNull(target);
        }

        private void Awake()
        {
            AssertInspectorVars();
        }

        private void Start()
        {
            targetPosDiff = transform.position - target.position;
            isFollowing = true;
        }

        private void Update()
        {
            if (!isFollowing)
                return;

            Vector3 newPos = targetPosDiff + target.position;
            if (newPos.y <= minY)
            {
                newPos.x = transform.position.x;
                newPos.y = minY;
                newPos.z = transform.position.z;

                isFollowing = false;
            }
            transform.position = newPos;
        }

        public void StopFollowing()
        {
            isFollowing = false;
        }

    }

}
