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
        Debug.Log(collision.tag);
        if (collision.gameObject.tag == "Tail")
        {
            //Debug.Log("Tail");
        }
        if (collision.gameObject.tag == "Wall")
        {
           // Debug.Log("Wall");
        }

        if (collision.gameObject.tag == "Tail" || collision.gameObject.tag == "Wall")
        {
          //  Debug.Log(collision.name);
            //게임오버
        }

        if(collision.gameObject.tag == "Item")
        {
            //EventManager.Instance.CallOnUseItem(collision.gameObject);
            int num = collision.gameObject.GetComponent<ItemValue>().GetValue();
            Debug.Log(num);
            for (int i = 0; i < num; ++i)
            {
                EventManager.Instance.CallOnAddTail();
                Debug.Log("추가");
            }
            Destroy(collision.gameObject);
            Debug.Log("충돌");
        }
    }
}
