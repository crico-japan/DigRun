using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    [Header("速度")]
    private float speed = 0.2f;
    private Vector3 direction;

    protected float screenOutOffset = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed*Time.deltaTime;
        if (Utility.ChechInsideViewPortToWorldPoint(gameObject.transform, 0.5f) == false)
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }
}
