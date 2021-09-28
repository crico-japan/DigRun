using UnityEngine;

namespace Crico.AI
{
    public class SignalReceiver : MonoBehaviour
    {
        int[] signalCounts;

        private void Awake()
        {
            signalCounts = new int[(int)SignalType.NUM_SIGNALS];
        }

        public void AddSignals(SignalType inputMask)
        {
            for (int i = 0; i < (int)SignalType.NUM_SIGNALS; ++i)
            {
                SignalType current = (SignalType)(1 << i);
                if ((inputMask & current) != 0)
                {
                    ++signalCounts[i];
                }
            }

        }

        public void RemoveSignals(SignalType inputMask)
        {
            for (int i = 0; i < (int)SignalType.NUM_SIGNALS; ++i)
            {
                SignalType current = (SignalType)(1 << i);
                if ((inputMask & current) != 0)
                {
                    signalCounts[i] = Mathf.Max(0, signalCounts[i] - 1);
                }
            }
        }

        public bool IsSignalOn(SignalType type)
        {
            bool result = false;
            for (int i = 0; i < (int)SignalType.NUM_SIGNALS; ++i)
            {
                SignalType current = (SignalType)(1 << i);
                if ((type & current) != 0)
                {
                    result = signalCounts[i] > 0;
                    break;
                }
            }

            return result;
        }
    }

}
