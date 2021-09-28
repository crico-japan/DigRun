using UnityEngine;

namespace Crico
{
    public class ChangeLayerTo : MonoBehaviour
    {
        [SerializeField] int layerIndex = 0;
        [SerializeField] GameObject[] objectsToExclude = new GameObject[0];

        public void DoChange()
        {
            DoChange(transform);
        }

        private void DoChange(Transform current)
        {
            bool exclude = false;
            foreach (GameObject objectToExclude in objectsToExclude)
            {
                if (objectToExclude == current.gameObject)
                {
                    exclude = true;
                    break;
                }
            }

            if (exclude)
                return;

            if (current.childCount > 0)
            {
                foreach (Transform child in current)
                    DoChange(child);
            }

            current.gameObject.layer = layerIndex;
        }
    }

}
