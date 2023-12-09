using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HeadCollisionController : MonoBehaviour
{
    [SerializeField] HeadMove moveScript;

    //�浹 �Է� �߻� �� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�������� �˻�
        if (collision.gameObject.tag == "Tail")
        {
            if (moveScript.tails[0] != collision.gameObject)
            {
                EventManager.Instance.CallOnGameOver();
            }
        }
        if (collision.gameObject.tag == "Wall")
        {
            EventManager.Instance.CallOnGameOver();
        }

        // ������ ȹ�� �˻�
        if (collision.gameObject.tag == "Item")
        {
            EventManager.Instance.CallOnPlusScore();
            GameObject tail = collision.GetComponent<ItemInfoHolder>().getTail;
            EventManager.Instance.CallOnAddTail(tail);
            Destroy(collision.gameObject);
            EventManager.Instance.CallOnCreateItem();
        }
    }
}
