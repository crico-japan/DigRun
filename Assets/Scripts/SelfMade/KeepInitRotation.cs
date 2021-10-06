using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepInitRotation : MonoBehaviour
{
    Vector3 rotateDef;
    Vector3 worldPosBuf;
    private void Awake()
    {
        rotateDef = transform.localRotation.eulerAngles;
        worldPosBuf = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        Vector3 parent = transform.parent.transform.localRotation.eulerAngles;

        transform.localRotation = Quaternion.Euler(rotateDef - parent);
        transform.position = worldPosBuf;
    }
}
