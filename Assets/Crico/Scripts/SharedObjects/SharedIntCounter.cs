using UnityEngine;
using UnityEngine.Assertions;

namespace Crico.SharedObjects
{
    public class SharedIntCounter : MonoBehaviour
    {
        [SerializeField] SharedInt sharedValue;
        [SerializeField] int initial;
        [SerializeField] int min;
        [SerializeField] int max = 99;

        private void Awake()
        {
            Assert.IsNotNull(sharedValue);
            sharedValue.Set(initial);
        }

        public void SetValue(int value)
        {
            value = Mathf.Clamp(value, min, max);
            this.sharedValue.Set(value);
        }

        public void IncrementValue()
        {
            SetValue(sharedValue.value + 1);
        }

        public void DecrementValue()
        {
            SetValue(sharedValue.value - 1);
        }

    }

}
