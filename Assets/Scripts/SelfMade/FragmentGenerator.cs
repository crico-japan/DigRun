using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentGenerator : MonoBehaviour, IObserver
{
    [SerializeField]
    [Header("破片のプレハブ")]
    private GameObject[] fragmentPrefabs;

    //[SerializeField]
    //[Header("生成する破片の数の上限")]
    //private int fragmentMax = 10;
    //private int fragmentNum = 0;

    [SerializeField]
    DestructibleTerrain[] terrains;

    //通知を受け取った際の処理
    public void OnNotify(Subject subject)
    {
        var terrain = subject as DestructibleTerrain;
        if(terrain != null)
        {
            GenerateFragment();
        }

        //var digger = subject as Digger;

        //if(digger != null)
        //{
        //    GenerateFragment(digger.ClipperPos);
        //}
        //var fragment = subject as FragmentController;
        //if(fragment != null)
        //{
        //    fragmentNum--;
        //    if(fragmentNum < 0)
        //    {
        //        fragmentNum = 0;
        //    }
        //}
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
        touchPos.z = -Camera.main.transform.position.z;
        var worldPos = Camera.main.ScreenToWorldPoint(touchPos);
        var pos = new Vector3(worldPos.x + correction.x, worldPos.y + correction.y, 0.25f);
        var fragment = Instantiate(fragmentPrefabs[num], pos, Quaternion.Euler(Random.Range(0.0f, 180.0f), Random.Range(0.0f, 180.0f), 0.0f));
        //fragment.GetComponent<FragmentController>().AddObserver(this);
    }

    public void GenerateFragment(Vector3 pos)
    {
        for (int i = 0; i < 10; i++)
        {
            int num = Random.Range(0, fragmentPrefabs.Length);
            var correction = Random.insideUnitCircle * 1.0f;
            var generatePos = new Vector3(pos.x + correction.x, pos.y + correction.y, 0.25f);
            var fragment = Instantiate(fragmentPrefabs[num], generatePos, Quaternion.Euler(Random.Range(0.0f, 180.0f), Random.Range(0.0f, 180.0f), 0.0f));
        }       
    }
}
