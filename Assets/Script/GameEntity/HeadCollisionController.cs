using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HeadCollisionController : MonoBehaviour
{
    [SerializeField] HeadMove moveScript;

    //충돌 입력 발생 시 실행
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //게임종료 검사
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

        // 아이템 획득 검사
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
