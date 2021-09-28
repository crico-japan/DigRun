using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 監視される側
/// </summary>
[Serializable]
public abstract class Subject : MonoBehaviour
{
    [SerializeField]
    protected List<IObserver> observers = new List<IObserver>();

    protected void Notify()
    {
        observers.ForEach(observers => observers.OnNotify(this));
    }

    public void AddObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }
}