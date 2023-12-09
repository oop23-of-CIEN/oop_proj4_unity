using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollisionController : MonoBehaviour
{
    [SerializeField] HeadMove moveScript;
    [SerializeField] GameUIController uiController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.gameObject.tag == "Tail")
        {
            if (moveScript.tails[0] != collision.gameObject)
            {
                //��������
                uiController.SetGameOver();
            }
        }
        if (collision.gameObject.tag == "Wall")
        {
            //��������
            //uiController.SetGameOver();
            EventManager.Instance.CallOnGameOver();
        }


        if (collision.gameObject.tag == "Item")
        {
            
            uiController.GetScore();
            GameObject tail = collision.GetComponent<ItemInfoHolder>().getTail;
            EventManager.Instance.CallOnAddTail(tail);
            
            Destroy(collision.gameObject);

            EventManager.Instance.CallOnCreateItem();
        }
    }
}
