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

    ////�c�A���A�������͗����̂ǂ����ɂ��邩
    //private enum BaseType
    //{
    //    Both,
    //    Width,
    //    Height
    //}

    //[SerializeField]
    //private BaseType baseType = BaseType.Both;

    //[SerializeField]
    //private float baseWidth = 720.0f, baseHeight = 1280.0f;

    //[SerializeField]
    //private bool isAlwaysUpdate = false;

    //���݂̃A�X�y�N�g��
    private float currentAspect;

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

        //var leftBottomScreenPos = RectTransformUtility.WorldToScreenPoint(camera, leftBottom.position);
        //var rightTopScreenPos = RectTransformUtility.WorldToScreenPoint(camera, rightTop.position);

        //baseHeight = rightTopScreenPos.y - leftBottomScreenPos.y;
        //baseWidth = rightTopScreenPos.x - leftBottomScreenPos.x;

        ////��̃A�X�y�N�g��Ɗ�̃A�X�y�N�g��̎���Size
        //float baseAspect = baseHeight / baseWidth;
        //float baseOrthographicSize = baseHeight / 100 / 2f;

        //UpdateOrthographicSize();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(!isAlwaysUpdate && Application.isPlaying)
        //{
        //    return;
        //}

        //UpdateOrthographicSize();
    }

    //�C���X�y�N�^�[�̒l���ύX���ꂽ���Ɏ��s�AOrthographicSize���X�V����
    private void OnValidate()
    {
        //currentAspect = 0;
        //UpdateOrthographicSize();
    }

    //private void UpdateOrthographicSize()
    //{
    //    //���݂̃A�X�y�N�g����擾���A�ω���������΍X�V���Ȃ�
    //    float currentAspect = baseHeight / baseWidth;
    //    if(Mathf.Approximately(this.currentAspect, currentAspect))
    //    {
    //        return;
    //    }

    //    this.currentAspect = currentAspect;

    //    if(camera == null)
    //    {
    //        camera = GetComponent<Camera>();
    //    }

    //    //��̃A�X�y�N�g��Ɗ�̃A�X�y�N�g��̎���Size
    //    float baseAspect = baseHeight / baseWidth;
    //    float baseOrthographicSize;
    //    if (baseHeight > baseWidth)
    //    {
    //        baseOrthographicSize = baseHeight / 100 / 2f;
    //    }
    //    else
    //    {
    //        baseOrthographicSize = baseWidth / 100 / 2;
    //    }

    //    //�J������OrthographicSize��ݒ肵�Ȃ���
    //    if (baseType == BaseType.Height || (baseAspect > currentAspect && baseType !=BaseType.Width))
    //    {
    //        camera.orthographicSize = baseOrthographicSize;
    //    }
    //    else
    //    {
    //        camera.orthographicSize = baseOrthographicSize * (currentAspect / baseAspect);
    //    }
    //}
}
