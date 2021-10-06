using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Animator))]
public class TreasureChestScript : MonoBehaviour
{
    private Animator animator = null;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //public void Open()
    //{
    //    animator.Play("Open");
    //}
}
