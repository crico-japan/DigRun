using UnityEngine;
using UnityEngine.Assertions;

namespace Crico
{
    public class ObjectFactory : MonoBehaviour
    {
        [SerializeField] private GameObject prefab = null;

        private void AssertInspectorVars()
        {
            Assert.IsNotNull(prefab);
        }

        private void Awake()
        {
            AssertInspectorVars();
        }

        public void CreateObjectAtPosition(Vector3 position)
        {
            GameObject newObject = Instantiate(prefab);
            newObject.transform.position = position;
        }

    }

}
