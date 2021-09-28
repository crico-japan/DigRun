using UnityEngine;

namespace Crico.AI
{
    public class Trigger : MonoBehaviour
    {
        [SerializeField] bool setOnStart = false;

        public bool triggerValue { get; private set; }

        public void SetTrigger()
        {
            triggerValue = true;
        }

        public void ConsumeTrigger()
        {
            triggerValue = false;
        }

        private void Start()
        {
            if (setOnStart)
                SetTrigger();
        }


    }

}
