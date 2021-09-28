using UnityEngine;
using UnityEngine.Events;

namespace Crico.Combat
{
    [RequireComponent(typeof(HPHolder))]
    public class InvokeOnHPZero : MonoBehaviour
    {
        [SerializeField] UnityEvent eventToInvoke = new UnityEvent();

        bool triggered;

        private void Awake()
        {
            GetComponent<HPHolder>().AddListener(OnTakeDamage);
        }

        private void OnTakeDamage(int hp)
        {
            if (triggered)
                return;

            if (hp <= 0)
            {
                triggered = true;
                eventToInvoke.Invoke();
            }
        }

    }

}
