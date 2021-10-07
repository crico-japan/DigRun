using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RockFracture : MonoBehaviour
{
    [SerializeField]
    Collider2D trigger = null;
    public Collider2D Trigger
    {
        get { return trigger; }
    }

    [SerializeField]
    Collider2D collison = null;
    public Collider2D Collision
    {
        get { return collison; }
    }

    [SerializeField]
    new Rigidbody2D rigidbody;
    public Rigidbody2D Rigidbody
    {
        get { return rigidbody; }
    }

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScaleDown()
    {
        transform.DOScale(Vector3.zero, 2.5f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
