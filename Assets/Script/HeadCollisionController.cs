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
                //게임종료
                uiController.SetGameOver();
            }
        }
        if (collision.gameObject.tag == "Wall")
        {
            //게임종료
            uiController.SetGameOver();
        }


        if (collision.gameObject.tag == "Item")
        {
            //EventManager.Instance.CallOnUseItem(collision.gameObject);
            int num = collision.gameObject.GetComponent<ItemValue>().GetValue();
            //점수추가
            uiController.GetScore();
            Debug.Log(num);
            /*
             * 일단 아이템 획득시 꼬리 1개 생성을 위해 주석처리
            for (int i = 0; i < num; ++i)
            {
                EventManager.Instance.CallOnAddTail();
                Debug.Log("추가");
            }
            */
            Destroy(collision.gameObject);
            Debug.Log("충돌");
        }
    }
}
