using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MiniMapIcon : MonoBehaviour
{
    [SerializeField]
    [Header("アイコンに対応するオブジェクト")]
    private Transform iconTarget = null;

    [SerializeField]
    private float rangeRadiusOffset = 1.0f;

    private Camera minimapCamera;
    private float minimapRangeRadius;     //アイコンのデフォルトY
    private float defaultPosZ;

    private void Awake()
    {
        minimapCamera = GameObject.FindGameObjectWithTag(TagName.MiniMapCamera).GetComponent<Camera>();
        minimapRangeRadius = minimapCamera.orthographicSize;
        defaultPosZ = transform.position.z;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DispIcon();
    }

    /// <summary>
    /// アイコンを表示する
    /// </summary>
    private void DispIcon()
    {
        var iconPos = new Vector3(iconTarget.position.x, iconTarget.position.y, defaultPosZ);

        if(CheckInsideMap())
        {
            transform.position = iconPos;
            return;
        }
        var centerPos = new Vector3(minimapCamera.transform.position.x, minimapCamera.transform.position.y, defaultPosZ);
        var offset = iconPos - centerPos;
        transform.position = centerPos + Vector3.ClampMagnitude(offset, minimapRangeRadius - rangeRadiusOffset);
    }

    /// <summary>
    /// ミニマップの範囲内かチェックする
    /// </summary>
    /// <returns></returns>
    private bool CheckInsideMap()
    {
        var cameraPos = minimapCamera.transform.position;
        var targetPos = iconTarget.position;

        cameraPos.z = targetPos.z = 0;
        return Vector3.Distance(cameraPos, targetPos) <= minimapRangeRadius - rangeRadiusOffset;
    }
}
