using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Camera))]
public class MiniMapCamera : MonoBehaviour
{
    [SerializeField]
    Transform leftBottom = null;

    [SerializeField]
    Transform rightTop = null;

    private Camera camera;
    public void Init(float width, float height)
    {
        transform.position = new Vector3(width / 2, height / 2, transform.position.z);
    }

    public void Init(Vector2 leftBottom, Vector2 rightTop)
    {
        float width = Mathf.Abs(rightTop.x - leftBottom.x);
        float height = Mathf.Abs(rightTop.y - leftBottom.y);
        transform.position = new Vector3(width / 2, height / 2, transform.position.z);
    }

    private void AssertInspectorVars()
    {
        Assert.IsNotNull(leftBottom);
        Assert.IsNotNull(rightTop);
    }
    private void Awake()
    {
        AssertInspectorVars();
        float width = Mathf.Abs(rightTop.position.x - leftBottom.position.x);
        float height = Mathf.Abs(rightTop.position.y - leftBottom.position.y);
        float x = leftBottom.position.x + (width / 2);
        float y = leftBottom.position.y + (height / 2);
        transform.position = new Vector3(x, y, transform.position.z);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
