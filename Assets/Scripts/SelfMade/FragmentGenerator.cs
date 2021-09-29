using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentGenerator : MonoBehaviour, IObserver
{
    [SerializeField]
    float fragmentScale = 1.0f;

    [SerializeField]
    [Header("破片のプレハブ")]
    private GameObject[] fragmentPrefabs;

    [SerializeField]
    DestructibleTerrain[] terrains;



    private Camera mainCamera;
    private float cameraZ;

    //通知を受け取った際の処理
    public void OnNotify(Subject subject)
    {
        var terrain = subject as DestructibleTerrain;
        if(terrain != null)
        {
            GenerateFragment();
        }
    }

    public void Init(Camera camera)
    {
        mainCamera = camera;
        cameraZ = Mathf.Abs(mainCamera.transform.position.z - terrains[0].transform.position.z);
    }
    // Start is called before the first frame update
    void Start()
    {
       foreach(var item in terrains)
        {
            item.AddObserver(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateFragment()
    {
        int num = Random.Range(0, fragmentPrefabs.Length);
        var correction = Random.insideUnitCircle * 0.5f;
        var touch = TouchUtility.GetTouch(0);
        Vector3 touchPos = touch.position;
        touchPos.z = cameraZ;
        var worldPos = mainCamera.ScreenToWorldPoint(touchPos);
        var pos = new Vector3(worldPos.x + correction.x, worldPos.y + correction.y, worldPos.z + 0.25f);
        var fragment = Instantiate(fragmentPrefabs[num], gameObject.transform);
        fragment.transform.position = pos;
        fragment.transform.rotation = Quaternion.Euler(Random.Range(0.0f, 180.0f), Random.Range(0.0f, 180.0f), 0.0f);
        fragment.transform.localScale = new Vector3(fragmentScale, fragmentScale, fragmentScale);
    }

    public void GenerateFragment(Vector3 pos)
    {
        int num = Random.Range(0, fragmentPrefabs.Length);
        var correction = Random.insideUnitCircle * 0.5f;
        var generatePos = new Vector3(pos.x + correction.x, pos.y + correction.y, 0.25f);
        var fragment = Instantiate(fragmentPrefabs[num], gameObject.transform);
        fragment.transform.position = generatePos;
        fragment.transform.rotation = Quaternion.Euler(Random.Range(0.0f, 180.0f), Random.Range(0.0f, 180.0f), 0.0f);
    }
}
