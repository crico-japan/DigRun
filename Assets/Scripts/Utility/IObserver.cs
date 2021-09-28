using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 監視する側
/// </summary>
public interface IObserver
{
    void OnNotify(Subject subject);
}
