using UnityEngine;

namespace Crico
{
    public class DestroyObjectOnReleaseMode : MonoBehaviour
    {

        private void Awake()
        {
            if (!Debug.isDebugBuild)
            {
                Destroy(gameObject);
            }
            
        }
    }

}
