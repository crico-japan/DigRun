using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAnimationEvent : MonoBehaviour
{
    [SerializeField]
    private Transform generatePos = null;

    [SerializeField]
    private GameObject bulletPrefab = null;
    public void Fire()
    {
        var bullet = Instantiate(bulletPrefab, new Vector3(generatePos.position.x, generatePos.position.y, generatePos.position.z), Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(Vector3.left);
        //if(enemy.Direction.x > 0)
        //{
        //    bullet.transform.localRotation = Quaternion.AngleAxis(180, Vector3.up);
        //}
    }
}
