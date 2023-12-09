using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeadCollisionController : MonoBehaviour
{
    [SerializeField] HeadMove moveScript;
    [SerializeField] GameUIController uiController;
    [SerializeField] AudioSource itemPickupSound;

    [SerializeField] AudioClip[] itemPickupEffects;
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
            //uiController.SetGameOver();
            EventManager.Instance.CallOnGameOver();
        }


        if (collision.gameObject.tag == "Item")
        {
            int randN = Random.Range(0, itemPickupEffects.Count());
            itemPickupSound.clip = itemPickupEffects[randN];
            itemPickupSound.Play();
            uiController.GetScore();
            GameObject tail = collision.GetComponent<ItemInfoHolder>().getTail;
            EventManager.Instance.CallOnAddTail(tail);
            
            Destroy(collision.gameObject);

            EventManager.Instance.CallOnCreateItem();
        }
    }
}
