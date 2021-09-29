using Crico.AI;
using Crico.AI.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFreeAgent : MonoBehaviour
{
    [SerializeField]
    Transform left;

    [SerializeField]
    Transform right;

    [SerializeField]
    int numPoints = 10;

    [SerializeField]
    float moveSpeed = 2.5f;

    [SerializeField]
    float rayLength = 0.5f;

    [SerializeField]
    Transform bottom = null;

    List<Transform> rayPoints = new List<Transform>();
    float width;
    float space;

    public bool isRunning = false;

    int layerMask = 1 << LayerNameMask.GroundMask;

    private void Awake()
    {
        width = Mathf.Abs(right.position.x - left.position.x);
        space = width / numPoints;

        for (int i = 0; i < numPoints; i++)
        {
            GameObject child = new GameObject();
            child.name = "RayPoint";
            child.transform.SetParent(transform);
            child.transform.position = new Vector3(left.position.x + (i * space), left.position.y, left.position.z);
            rayPoints.Add(child.transform);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    //Update is called once per frame
    void Update()
    {
        normal = Vector3.up;

        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.position = transform.position + (transform.right * moveSpeed * Time.fixedDeltaTime);
        //}
        //else if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.position = transform.position + (transform.right * -moveSpeed * Time.fixedDeltaTime);
        //}

        RaycastHit hit;

        foreach (var point in rayPoints)
        {
            if (Physics.Raycast(point.position, -transform.up.normalized, out hit, rayLength, layerMask))
            {
                //念のためzの値を消去
                var normalbuf = hit.normal;
                normalbuf.z = 0;
                normal += normalbuf;
            }
            else
            {
                //normal += transform.up.normalized;
                normal += Vector3.up;
            }
        }
    }

    private void FixedUpdate()
    {
        if(isRunning == false)
        {
            return;
        }
        //傾きを求める
        Quaternion q = Quaternion.FromToRotation(transform.up, normal.normalized);

        //自身を回転させる
        transform.rotation *= q;

        transform.position = transform.position + (transform.right * moveSpeed * Time.fixedDeltaTime);

        RaycastHit hit;
        if (Physics.Raycast(bottom.position, -transform.up, out hit, 0.5f, layerMask))
        {
            if (hit.distance > 0.05f)
            {
                transform.position = transform.position + (-transform.up * Physics.gravity.magnitude * Time.fixedDeltaTime);
            }
        }
    }

    [SerializeField]
    Vector3 normal;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var point in rayPoints)
        {
            Gizmos.DrawSphere(point.position, 0.1f);
        }

        Gizmos.DrawRay(transform.position, transform.position + (normal*rayLength));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(bottom.position, bottom.position + (-transform.up*3));

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (transform.right * 3));
        //Gizmos.color = Color.blue;
        //Gizmos.DrawRay(transform.position, transform.forward);
        //Gizmos.DrawRay()
    }
}
