using UnityEngine;

namespace Crico.AI
{
    [System.Serializable]
    public abstract class AgentProperty<T>
    {
        [SerializeField] protected string name;
        [SerializeField] protected T property;

        public bool CheckMatch(string name)
        {
            return string.Equals(this.name, name);
        }

        public string GetName()
        {
            return name;
        }

        public T GetProperty()
        {
            return property;
        }

        public void SetProperty(T value)
        {
            this.property = value;
        }
    }

}
