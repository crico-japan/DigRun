using UnityEngine;

namespace Crico.AI
{
    [RequireComponent(typeof(Rigidbody))]
    public class SignalTransmitter : MonoBehaviour
    {
        [SerializeField] SignalReceiverLocation[] locationsToIgnore = new SignalReceiverLocation[] { };
        [SerializeField] SignalType types = SignalType.NONE;

        public void Disable()
        {
            GetComponent<Rigidbody>().detectCollisions = false;
        }

        private void TransmitSignal(Collider other)
        {
            SignalReceiverLocation signalReceiver = other.GetComponent<SignalReceiverLocation>();
            if (signalReceiver == null)
                return;

            if (IsLocationToBeIgnored(signalReceiver))
                return;

            signalReceiver.AddSignals(types);
        }

        private void UnTransmitSignal(Collider other)
        {
            SignalReceiverLocation signalReceiver = other.GetComponent<SignalReceiverLocation>();
            if (signalReceiver == null)
                return;

            if (IsLocationToBeIgnored(signalReceiver))
                return;

            signalReceiver.RemoveSignals(types);
        }

        private bool IsLocationToBeIgnored(SignalReceiverLocation candidate)
        {
            bool result = false;

            foreach (SignalReceiverLocation location in locationsToIgnore)
            {
                if (location == candidate)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        private void OnTriggerEnter(Collider other)
        {
            TransmitSignal(other);
        }

        private void OnTriggerExit(Collider other)
        {
            UnTransmitSignal(other);
        }

    }

}
