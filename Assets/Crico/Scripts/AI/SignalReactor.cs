using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Crico.AI
{
    public class SignalReactor : MonoBehaviour
    {
        [SerializeField] SignalReceiver receiver;
        [SerializeField] SignalType signalToDetect = SignalType.DUMMY;
        [SerializeField] UnityEvent onDetect = new UnityEvent();
        [SerializeField] UnityEvent continuous = new UnityEvent();

        bool signalDetectedLastFrame;

        private void Reset()
        {
            receiver = GetComponent<SignalReceiver>();
        }

        private void Awake()
        {
            Assert.IsNotNull(receiver);
        }

        private void Update()
        {
            bool signalOnThisFrame = receiver.IsSignalOn(signalToDetect);

            if (!signalDetectedLastFrame)
            {
                if (signalOnThisFrame)
                {
                    onDetect.Invoke();
                    signalDetectedLastFrame = true;
                }
            }
            else
            {
                if (signalOnThisFrame)
                {
                    continuous.Invoke();
                }
            }

            signalDetectedLastFrame = signalOnThisFrame;
        }



    }

}
