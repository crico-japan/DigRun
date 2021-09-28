using UnityEngine;
using UnityEngine.Events;

namespace Crico
{
    public class TriggerEnterEvent : MonoBehaviour
    {
        [System.Serializable]
        public class TriggerEvent : UnityEvent<GameObject> { };

        [SerializeField] string[] tagsToReactTo = new string[0];
        [SerializeField] GameObject[] gameObjectsToExclude = new GameObject[0];
        [SerializeField] bool oneShot = false;
        [SerializeField] TriggerEvent onTriggerEnter = new TriggerEvent();

        private void OnTriggerEnter(Collider collider)
        {
            bool hit = false;
            foreach (string tagToReactTo in tagsToReactTo)
            {
                if (collider.tag == tagToReactTo)
                {
                    hit = true;
                    break;
                }
            }

            if (!hit)
                return;

            bool miss = false;
            foreach (GameObject objectToExclude in gameObjectsToExclude)
            {
                if (collider.gameObject == objectToExclude)
                {
                    miss = true;
                    break;
                }
            }

            if (miss)
                return;

            onTriggerEnter.Invoke(collider.gameObject);

            if (oneShot)
                gameObject.SetActive(false);
        }
    }

}
