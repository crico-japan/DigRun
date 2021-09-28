using UnityEngine;

namespace Crico
{
    [System.Serializable]
    public class CodeDef
    {
        public char code = 'x';
        public bool burning = false;
        public GameObject prefabToPlace = null;
        public Transform parentToInsantiatePrefabTo = null;
        public float yRotOfPrefab = 0f;
    }

}
