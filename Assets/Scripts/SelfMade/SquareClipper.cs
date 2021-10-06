using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vector2i = ClipperLib.IntPoint;

public class SquareClipper : ClipperBase
{
    public DestructibleTerrain terrain;
    public float width;
    public float height;
    private Vector2 clipPosition;

    public override bool CheckBlockOverlapping(Vector2 p, float size)
    {
        float dx = Mathf.Abs(clipPosition.x - p.x) - width/2 - size / 2;
        float dy = Mathf.Abs(clipPosition.y - p.y) - height/2 - size / 2;

        return dx < 0f && dy < 0f;
    }

    public override ClipBounds GetBounds()
    {
        return new ClipBounds
        {
            lowerPoint = new Vector2(clipPosition.x - width/2, clipPosition.y - height/2),
            upperPoint = new Vector2(clipPosition.x + width/2, clipPosition.y + height/2)
        };
    }

    [SerializeField]
    List<Vector2i> vertices = new List<Vector2i>();

    public override List<Vector2i> GetVertices()
    {

        //左下
        Vector2 point = new Vector2(clipPosition.x - width / 2, clipPosition.y - height / 2);
        Vector2i point_i64 = point.ToVector2i();
        vertices.Add(point_i64);

        //右下
        point = new Vector2(clipPosition.x + width / 2, clipPosition.y - height / 2);
        point_i64 = point.ToVector2i();
        vertices.Add(point_i64);

        //右上
        point = new Vector2(clipPosition.x + width / 2, clipPosition.y + height / 2);
        point_i64 = point.ToVector2i();
        vertices.Add(point_i64);

        //左上
        point = new Vector2(clipPosition.x - width / 2, clipPosition.y + height / 2);
        point_i64 = point.ToVector2i();
        vertices.Add(point_i64);
        return vertices;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Vector2 positionWorldSpace = transform.position;
        if(terrain != null)
        {
            clipPosition = positionWorldSpace - terrain.GetPositionOffset();
            terrain.ExecuteClip(this, false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach(var vertex in vertices)
        {
            Gizmos.DrawSphere(vertex.ToVector3f(), 0.1f);
        }

    }
}
