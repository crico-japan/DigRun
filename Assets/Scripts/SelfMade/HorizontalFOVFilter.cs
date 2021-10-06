using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// êÖïΩéãñÏÇçáÇÌÇπÇÈ
/// </summary>
[RequireComponent(typeof(Camera))]
public class HorizontalFOVFilter : MonoBehaviour
{
    private Camera camera;

    [SerializeField]
    bool isDisable = false;

    [SerializeField]
    private Vector2 baseAspect = new Vector2(750, 1334);

    [SerializeField]
    private float baseFOV = 60.0f;
    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isDisable)
        {
            if(camera.fieldOfView != baseFOV)
            {
                camera.fieldOfView = baseFOV;
            }
            return;
        }

        float baseHorizontalFOV = CalcHorizontalFOV(baseFOV, CalcAspect(baseAspect.x, baseAspect.y));
        float currentAspect = CalcAspect(Screen.width, Screen.height);

        camera.fieldOfView = CalcVerticalFOV(baseHorizontalFOV, currentAspect);
    }

    private float CalcAspect(float width, float height)
    {
        return width / height;
    }

    private float CalcHorizontalFOV(float verticalFOV, float aspect)
    {
        return Mathf.Atan(Mathf.Tan(verticalFOV / 2f * Mathf.Deg2Rad) * aspect) * 2f * Mathf.Rad2Deg;
    }

    private float CalcVerticalFOV(float horizontalFOV, float aspect)
    {
        return Mathf.Atan(Mathf.Tan(horizontalFOV / 2f * Mathf.Deg2Rad) / aspect) * 2f * Mathf.Rad2Deg;    
    }

    //private void OnValidate()
    //{
    //    if (isDisable)
    //    {
    //        if (camera.fieldOfView != baseFOV)
    //        {
    //            camera.fieldOfView = baseFOV;
    //        }
    //        return;
    //    }

    //    float baseHorizontalFOV = CalcHorizontalFOV(baseFOV, CalcAspect(baseAspect.x, baseAspect.y));
    //    float currentAspect = CalcAspect(Screen.width, Screen.height);

    //    camera.fieldOfView = CalcVerticalFOV(baseHorizontalFOV, currentAspect);
    //}
}
