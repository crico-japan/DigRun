using UnityEngine;

namespace Crico
{
    public class SelfDestructor : MonoBehaviour
    {
        [SerializeField] private float lifetime = 0.5f;

        private void Update()
        {
            lifetime -= Time.deltaTime;
            if (lifetime <= 0f)
                Destroy(gameObject);
        }
    }
}
