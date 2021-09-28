using UnityEngine;

namespace Crico.SharedObjects
{
    public class SharedObject<T> : ScriptableObject
    {
        [SerializeField] T _value;
        [SerializeField] bool canChangeAtRunTime = true;

        public T value { get => _value; private set => _value = value; }

        public void Set(T value)
        {
            if (!canChangeAtRunTime)
                return;

            this.value = value;
        }
    }
}
