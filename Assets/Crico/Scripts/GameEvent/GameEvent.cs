using System.Collections.Generic;
using UnityEngine;

namespace Crico.GameEvents
{
    [CreateAssetMenu(menuName = "Crico/GameEvent")]
    public class GameEvent : ScriptableObject
    {
        private List<IGameEventListener> listeners = new List<IGameEventListener>();

        public void RegisterListener(IGameEventListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(IGameEventListener listener)
        {
            listeners.Remove(listener);
        }

        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; --i)
                listeners[i].OnEventRaised();
        }

    }

    public class GameEvent<T0> : ScriptableObject
    {
        private List<IGameEventListener<T0>> listeners = new List<IGameEventListener<T0>>();

        public void RegisterListener(IGameEventListener<T0> listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(IGameEventListener<T0> listener)
        {
            listeners.Remove(listener);
        }

        public void Raise(T0 arg0)
        {
            for (int i = listeners.Count - 1; i >= 0; --i)
                listeners[i].OnEventRaised(arg0);
        }
    }

    public class GameEvent<T0, T1> : ScriptableObject
    {
        private List<IGameEventListener<T0, T1>> listeners = new List<IGameEventListener<T0, T1>>();

        public void RegisterListener(IGameEventListener<T0, T1> listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(IGameEventListener<T0, T1> listener)
        {
            listeners.Remove(listener);
        }

        public void Raise(T0 arg0, T1 arg1)
        {
            for (int i = listeners.Count - 1; i >= 0; --i)
                listeners[i].OnEventRaised(arg0, arg1);
        }
    }
}
