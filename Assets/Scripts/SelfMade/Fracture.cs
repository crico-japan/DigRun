using Crico.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fracture : MonoBehaviour
{
    [SerializeField]
    List<RockFracture> fractures;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            foreach(var obj in fractures)
            {
                obj.GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Agent>() != null)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;

            for (int i = 0; i < fractures.Count; i++)
            {
                fractures[i].Collision.gameObject.SetActive(true);
                fractures[i].Rigidbody.simulated = true;
                fractures[i].ScaleDown();
            }

            fractures[0].Rigidbody.AddForce(new Vector2(1, 2), ForceMode2D.Impulse);
            fractures[1].Rigidbody.AddForce(new Vector2(-1, 2), ForceMode2D.Impulse);
            fractures[2].Rigidbody.AddForce(new Vector2(1, 0), ForceMode2D.Impulse);
            fractures[3].Rigidbody.AddForce(new Vector2(-1, 0), ForceMode2D.Impulse);

        }
    }
}
