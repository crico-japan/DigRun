using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RockFracture : MonoBehaviour
{
    //[SerializeField]
    //Collider trigger = null;
    //public Collider Trigger
    //{
    //    get { return trigger; }
    //}

    [SerializeField]
    Collider collison = null;
    public Collider Collision
    {
        get { return collison; }
    }

    [SerializeField]
    new Rigidbody rigidbody;
    public Rigidbody Rigidbody
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
