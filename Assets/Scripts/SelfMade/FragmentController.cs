using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.DOScale(Vector3.zero, 1.0f)
            .OnComplete(() =>
            {
                this.transform.DOKill();
                Destroy(gameObject);
            });
    }
}
