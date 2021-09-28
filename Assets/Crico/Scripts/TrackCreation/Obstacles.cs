using UnityEngine;

namespace Crico.TrackCreation
{
    public class Obstacles : MonoBehaviour
    {
        [SerializeField] public float initialDirectionPolarity = 1f;
        [SerializeField] public bool randomReactionDirectionPolarity = true;


        private void OnTriggerEnter(Collider other)
        {
            TrackRunner trackRunner = other.GetComponent<TrackRunner>();
            if (trackRunner != null)
            {
                if (trackRunner.isAtHighSpeed)
                {
                    BlowChildrenAway(trackRunner);
                }
                else
                {
                    Vector3 runnerFallDirection = trackRunner.transform.position - transform.position;
                    runnerFallDirection.Normalize();

                    float multiplier = initialDirectionPolarity;
                    if (randomReactionDirectionPolarity)
                    {
                        multiplier = Random.Range(0, 2) == 0 ? multiplier : -multiplier;
                    }
                    runnerFallDirection += multiplier * transform.right;
                    runnerFallDirection.Normalize();

                    trackRunner.StartFallingBack(runnerFallDirection);
                }
            }
        }

        private void BlowChildrenAway(TrackRunner trackRunner)
        {
#if UNITY_ANDROID || UNITY_IOS
            Handheld.Vibrate();
#endif
            float runnerSpeed = trackRunner.GetSpeed();

            float explosionForce = trackRunner.hitExplosionForce;
            float explosionRadius = trackRunner.hitExplosionRadius;
            Vector3 explosionPos = transform.position;

            foreach (Transform child in transform)
            {
                Rigidbody rigidbody = child.GetComponent<Rigidbody>();
                if (rigidbody == null)
                    continue;

                rigidbody.AddExplosionForce(explosionForce, explosionPos, explosionRadius);
            }
        }
    }
}
