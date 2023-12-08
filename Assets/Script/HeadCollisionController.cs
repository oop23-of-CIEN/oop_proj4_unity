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
            uiController.SetGameOver();
        }


        if (collision.gameObject.tag == "Item")
        {
            //EventManager.Instance.CallOnUseItem(collision.gameObject);
            int num = collision.gameObject.GetComponent<ItemValue>().GetValue();
            //�����߰�
            uiController.GetScore();
            Debug.Log(num);
            /*
             * �ϴ� ������ ȹ��� ���� 1�� ������ ���� �ּ�ó��
            for (int i = 0; i < num; ++i)
            {
                EventManager.Instance.CallOnAddTail();
                Debug.Log("�߰�");
            }
            */
            Destroy(collision.gameObject);
            Debug.Log("�浹");
        }
    }
}
