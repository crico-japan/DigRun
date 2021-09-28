using UnityEngine;

namespace Crico
{
    [RequireComponent(typeof(Rigidbody))]
    public class ForwardForceAdder : MonoBehaviour
    {
        public void AddForce(float amount)
        {
            Vector3 forceDir = transform.forward;
            Vector3 force = forceDir * amount;

            GetComponent<Rigidbody>().AddForce(force, ForceMode.VelocityChange);
        }
    }

}
