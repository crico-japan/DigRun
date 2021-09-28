using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace Crico
{
    public class RagdollRigidbodyHelper : MonoBehaviour
    {
        [SerializeField] GameObject rootObject = null;
        [SerializeField] bool setCollidersToTriggerOnInactive = false;
        [SerializeField] bool setBodiesInactiveOnStart = false;

        private Rigidbody[] modelBodies = null;

        public bool isActive { get; private set; } = true;

        private void Awake()
        {
            Assert.IsNotNull(rootObject);
        }

        private void Start()
        {
            if (setBodiesInactiveOnStart)
                SetModelBodiesActive(false);
        }

        private void StoreModelBodies()
        {
            if (modelBodies != null)
                return;

            modelBodies = rootObject.GetComponentsInChildren<Rigidbody>();
        }

        public void SetModelBodiesActive(bool value)
        {
            StoreModelBodies();

            if (isActive == value)
                return;

            isActive = value;

            bool isKinematic = !value;
            foreach (Rigidbody rigidbody in modelBodies)
            {
                rigidbody.isKinematic = isKinematic;

                if (setCollidersToTriggerOnInactive)
                    rigidbody.GetComponent<Collider>().isTrigger = isKinematic;
            }
        }

        public void SetModelBodiesToLayer(int layer)
        {
            SetModelBodiesToLayer(rootObject.transform, layer);
        }

        private void SetModelBodiesToLayer(Transform currentObject, int layer)
        {
            currentObject.gameObject.layer = layer;

            if (currentObject.transform.childCount == 0)
                return;

            foreach (Transform child in currentObject)
            {
                SetModelBodiesToLayer(child, layer);
            }
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(RagdollRigidbodyHelper))]
        public class RagdollRigidbodyHelperEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                if (GUILayout.Button("SetRigidBodiesActive"))
                {
                    RagdollRigidbodyHelper myScript = (RagdollRigidbodyHelper)target;
                    myScript.SetModelBodiesActive(true);
                }

                if (GUILayout.Button("SetRigidBodiesInactive"))
                {
                    RagdollRigidbodyHelper myScript = (RagdollRigidbodyHelper)target;
                    myScript.SetModelBodiesActive(false);
                }

            }
        }
#endif


    }



}
