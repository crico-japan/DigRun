using UnityEngine;
using UnityEngine.Events;

namespace Crico
{
    public class OnEnableDisableEvent : MonoBehaviour
    {
        [SerializeField] UnityEvent onEnabled = new UnityEvent();
        [SerializeField] UnityEvent onDisabled = new UnityEvent();

        private void OnEnable()
        {
            onEnabled.Invoke();
        }

        private void OnDisable()
        {
            onDisabled.Invoke();
        }
    }

}
