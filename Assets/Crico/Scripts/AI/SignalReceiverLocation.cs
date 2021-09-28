using UnityEngine;
using UnityEngine.Assertions;

namespace Crico.AI
{
    public class SignalReceiverLocation : MonoBehaviour
    {
        SignalReceiver signalReceiver = null;

        private void Awake()
        {
            signalReceiver = FindSignalReceiver();
            Assert.IsNotNull(signalReceiver);
        }

        private SignalReceiver FindSignalReceiver()
        {
            SignalReceiver result = GetComponent<SignalReceiver>();

            Transform parent = transform.parent;
            while (result == null && parent != null)
            {
                result = parent.GetComponent<SignalReceiver>();
                parent = parent.parent;
            }

            return result;
        }

        public void AddSignals(SignalType inputMask)
        {
            signalReceiver.AddSignals(inputMask);
        }

        public void RemoveSignals(SignalType inputMask)
        {
            signalReceiver.RemoveSignals(inputMask);
        }

        public bool IsSignalOn(SignalType type)
        {
            return signalReceiver.IsSignalOn(type);
        }

    }

}
