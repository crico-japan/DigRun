using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Crico
{
    [DisallowMultipleComponent, RequireComponent(typeof(Collider))]
    public class TriggerEventRelay : MonoBehaviour
    {
        [SerializeField] List<string> confineToTheseTags = new List<string>();

        [System.Serializable]
        public class TriggerEvent : UnityEvent<TriggerEventRelay, Collider> { };

        [SerializeField] TriggerEvent _onTriggerEnter = new TriggerEvent();
        [SerializeField] TriggerEvent _onTriggerStay = new TriggerEvent();
        [SerializeField] TriggerEvent _onTriggerExit = new TriggerEvent();
        public TriggerEvent onTriggerEnter { get => _onTriggerEnter; }
        public TriggerEvent onTriggerStay { get => _onTriggerStay; }
        public TriggerEvent onTriggerExit { get => _onTriggerExit; }

        public new Collider collider { get; private set; }

        private void Awake()
        {
            collider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (confineToTheseTags.Count > 0 && !confineToTheseTags.Contains(other.tag))
                return;

            try
            {
                _onTriggerEnter.Invoke(this, other);
            }
            catch (Exception e)
            {
                Debug.LogError("Exception! ("+gameObject.name + "): " + e.StackTrace);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (confineToTheseTags.Count > 0 && !confineToTheseTags.Contains(other.tag))
                return;

            try
            {
                _onTriggerStay.Invoke(this, other);
            }
            catch (Exception e)
            {
                Debug.LogError("Exception! (" + gameObject.name + "): " + e.StackTrace);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (confineToTheseTags.Count > 0 && !confineToTheseTags.Contains(other.tag))
                return;

            try
            {
                _onTriggerExit.Invoke(this, other);
            }
            catch (Exception e)
            {
                Debug.LogError("Exception! (" + gameObject.name + "): " + e.StackTrace);
            }
        }


    }

}
