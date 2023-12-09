using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Debugballmoving : MonoBehaviour
{
    [SerializeField] private KeyCode _up;
    [SerializeField] private KeyCode _down;
    [SerializeField] private KeyCode _right;
    [SerializeField] private KeyCode _left;



    private float _velocity = 4.0f;



    void Update()
    {

        if (Input.GetKey(_up))
        {
            transform.Translate(Vector3.up * _velocity * Time.deltaTime);
            
        }
        else if (Input.GetKey(_down))
        {
            Vector3 dir = Vector3.down * _velocity * Time.deltaTime;
            transform.Translate(dir);
            
            
        }
        else if (Input.GetKey(_left))
        {
            transform.Translate(Vector3.left * _velocity * Time.deltaTime);

            
        }
        else if (Input.GetKey(_right))
        {
            transform.Translate(Vector3.right * _velocity * Time.deltaTime);
           
        }
    }

    /*
      void Update()
     {

         if (Input.GetKey(_up))
         {
             transform.Translate(Vector3.up * _velocity * Time.deltaTime);
             _playerController.UpdateMove(Vector3.Magnitude(Vector3.up * _velocity * Time.deltaTime));
         }
         else if(Input.GetKey(_down))
         {
             transform.Translate(Vector3.down * _velocity * Time.deltaTime);
             _playerController.UpdateMove(Vector3.Magnitude(Vector3.down * _velocity * Time.deltaTime));
         }
         else if (Input.GetKey(_left))
         {
             transform.Translate(Vector3.left * _velocity * Time.deltaTime);

             _playerController.UpdateMove(Vector3.Magnitude(Vector3.left * _velocity * Time.deltaTime));
         }
         else if(Input.GetKey(_right))
         {
             transform.Translate(Vector3.right * _velocity * Time.deltaTime);
             _playerController.UpdateMove(Vector3.Magnitude(Vector3.right * _velocity * Time.deltaTime));
         }
     } 
     **/
}
