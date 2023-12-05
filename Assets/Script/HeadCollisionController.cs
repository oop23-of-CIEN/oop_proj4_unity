using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollisionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.gameObject.tag == "Tail")
        {
            Debug.Log("Tail");
        }

        if(collision.gameObject.tag == "Tail" || collision.gameObject.tag == "Wall")
        {
            Debug.Log(collision.name);
            //게임오버
        }
    }
}
