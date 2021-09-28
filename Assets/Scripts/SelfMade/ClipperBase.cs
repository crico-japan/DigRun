using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vector2i = ClipperLib.IntPoint;

public abstract class ClipperBase : MonoBehaviour, IClip
{
    public abstract bool CheckBlockOverlapping(Vector2 p, float size);

    public abstract ClipBounds GetBounds();

    public abstract List<Vector2i> GetVertices();

    public abstract void Init();
}
